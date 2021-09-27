using GuildCars.Data.Factory;
using GuildCars.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var vehicleRepo = VehicleRepositoryFactory.GetRepository();
            var specialsRepo = SpecialRepositoryFactory.GetRepository();

            var model = new HomeVM();

            model.Featured = vehicleRepo.GetFeaturedVehicles();
            model.Specials = specialsRepo.GetAll();

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact(string vin)
        {
            var model = new ContactVM();

            model.Vin = vin;

            return View(model);
        }

        [HttpPost]
        public ActionResult Contact(ContactVM model)
        {
            if (ModelState.IsValid)
            {
                var customerRepo = CustomerRepositoryFactory.GetRepository();
                var contactRepo = ContactRepositoryFactory.GetRepository();

                try
                {
                    customerRepo.Insert(model.Customer);
                    model.Contact.CustomerId = model.Customer.CustomerId;
                    contactRepo.Insert(model.Contact);
                    return RedirectToAction("Contact");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Specials()
        {
            var repo = SpecialRepositoryFactory.GetRepository();

            var model = repo.GetAll();

            return View(model);
        }
    }
}