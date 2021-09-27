using GuildCars.Data.Factory;
using GuildCars.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    public class InventoryController : Controller
    {
        // GET: Inventory
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult New()
        {
            InventorySearchVM model = new InventorySearchVM();

            return View(model);
        }

        [HttpGet]
        public ActionResult Used()
        {
            InventorySearchVM model = new InventorySearchVM();

            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var repo = VehicleRepositoryFactory.GetRepository();
            var model = repo.GetDetails(id);

            return View(model);
        }
    }
}