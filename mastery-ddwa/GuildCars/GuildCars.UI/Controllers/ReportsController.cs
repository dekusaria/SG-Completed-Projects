using GuildCars.Data.Factory;
using GuildCars.UI.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    public class ReportsController : Controller
    {
        [Authorize(Roles = "admin")]
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Sales()
        {
            var userMgr = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var users = new Dictionary<string, string>();

            foreach(var user in userMgr.Users)
            {
                users.Add(user.Email, $"{user.FirstName} {user.LastName}");
            }

            var model = new SelectList(users, "Key", "Value");

            return View(model);
        }

        [HttpGet]
        public ActionResult Inventory()
        {
            var model = new InventoryReportVM();

            var reportsRepo = ReportsRepositoryFactory.GetRepository();

            model.NewInventory = reportsRepo.GetInventoryByVehicleType(1);
            model.UsedInventory = reportsRepo.GetInventoryByVehicleType(2);

            return View(model);
        }
    }
}