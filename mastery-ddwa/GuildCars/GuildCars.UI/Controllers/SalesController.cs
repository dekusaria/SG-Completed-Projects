using GuildCars.Data.Factory;
using GuildCars.UI.Models;
using GuildCars.UI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    [Authorize(Roles = "sales, admin")]
    public class SalesController : Controller
    {
        // GET: Sales
        public ActionResult Index()
        {
            var model = new InventorySearchVM();

            return View(model);
        }

        [HttpGet]
        public ActionResult Purchase(int id)
        {
            var model = new PurchaseVM();

            var vehicleRepo = VehicleRepositoryFactory.GetRepository();
            var statesRepo = StatesRepositoryFactory.GetRepository();
            var purchaseTypeRepo = PurchaseTypeRepositoryFactory.GetRepository();

            model.Vehicle = vehicleRepo.GetDetails(id);
            model.States = new SelectList(statesRepo.GetAll(), "StateId", "StateId");
            model.PurchaseTypes = new SelectList(purchaseTypeRepo.GetAll(), "PurchaseTypeId", "PurchaseTypeName");

            return View(model);
        }

        [HttpPost]
        public ActionResult Purchase(PurchaseVM model)
        {
            if (ModelState.IsValid)
            {
                var vehicleRepo = VehicleRepositoryFactory.GetRepository();
                var customerRepo = CustomerRepositoryFactory.GetRepository();
                var addressRepo = AddressRepositoryFactory.GetRepository();
                var purchaseRepo = PurchaseRepositoryFactory.GetRepository();

                try
                {
                    vehicleRepo.VehicleIsSold(model.Vehicle.VehicleId);

                    addressRepo.Insert(model.Address);

                    model.Customer.AddressId = model.Address.AddressId;

                    customerRepo.Insert(model.Customer);

                    model.Purchase.CustomerId = model.Customer.CustomerId;
                    model.Purchase.VehicleId = model.Vehicle.VehicleId;
                    model.Purchase.PurchaseDate = DateTime.Today;
                    model.Purchase.SoldByEmail = AuthorizeUtilities.GetUserEmail(this);

                    purchaseRepo.Insert(model.Purchase);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                var vehicleRepo = VehicleRepositoryFactory.GetRepository();
                var statesRepo = StatesRepositoryFactory.GetRepository();
                var purchaseTypeRepo = PurchaseTypeRepositoryFactory.GetRepository();

                model.Vehicle = vehicleRepo.GetDetails(model.Vehicle.VehicleId);
                model.States = new SelectList(statesRepo.GetAll(), "StateId", "StateId");
                model.PurchaseTypes = new SelectList(purchaseTypeRepo.GetAll(), "PurchaseTypeId", "PurchaseTypeName");

                return View(model);
            }
        }
    }
}