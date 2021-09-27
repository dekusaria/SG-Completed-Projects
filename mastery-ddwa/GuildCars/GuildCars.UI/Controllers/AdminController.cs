using GuildCars.Data.Factory;
using GuildCars.Models.Tables;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Models
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            var model = new InventorySearchVM();

            return View(model);
        }

        [HttpGet]
        public ActionResult AddVehicle()
        {
            var model = new VehicleAddVM();

            var vehicleRepo = VehicleRepositoryFactory.GetRepository();
            var makesRepo = MakeRepositoryFactory.GetRepository();
            var vehicleTypesRepo = VehicleTypeRepositoryFactory.GetRepository();
            var bodyStylesRepo = BodyStyleRepositoryFactory.GetRepository();
            var transmissionsRepo = TransmissionFactoryRepository.GetRepository();
            var colorsRepo = ColorsRepositoryFactory.GetRepository();
            var interiorsRepo = InteriorsRepositoryFactory.GetRepository();

            model.Makes = new SelectList(makesRepo.GetAll(), "MakeId", "MakeName");
            model.VehicleTypes = new SelectList(vehicleTypesRepo.GetAll(), "VehicleTypeId", "VehicleTypeName");
            model.BodyStyles = new SelectList(bodyStylesRepo.GetAll(), "BodyStyleId", "BodyStyleType");
            model.Transmissions = new SelectList(transmissionsRepo.GetAll(), "TransmissionId", "TransmissionType");
            model.Colors = new SelectList(colorsRepo.GetAll(), "ColorId", "ColorName");
            model.Interiors = new SelectList(interiorsRepo.GetAll(), "InteriorId", "InteriorType");
            model.Vehicle = new Vehicle();

            return View(model);
        }

        [HttpPost]
        public ActionResult AddVehicle(VehicleAddVM model)
        {
            if (ModelState.IsValid)
            {
                if (model.VinUniqueFlag)
                {
                    var vehicleRepo = VehicleRepositoryFactory.GetRepository();

                    try
                    {
                        if (model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
                        {
                            var savepath = Server.MapPath("~/Images");

                            string fileName = "inventory-" + model.Vehicle.Vin;
                            string extension = Path.GetExtension(model.ImageUpload.FileName);

                            var filePath = Path.Combine(savepath, fileName + extension);

                            if (System.IO.File.Exists(filePath))
                            {
                                System.IO.File.Delete(filePath);
                            }

                            model.ImageUpload.SaveAs(filePath);

                            model.Vehicle.ImageFileName = Path.GetFileName(filePath);
                        }

                        vehicleRepo.Insert(model.Vehicle);

                        return RedirectToAction("EditVehicle", new { id = model.Vehicle.VehicleId });
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    TempData["UserMessages"] = "Vehicle Failed to Insert: VIN # Unique Constraint";

                    var vehicleRepo = VehicleRepositoryFactory.GetRepository();
                    var makesRepo = MakeRepositoryFactory.GetRepository();
                    var vehicleTypesRepo = VehicleTypeRepositoryFactory.GetRepository();
                    var bodyStylesRepo = BodyStyleRepositoryFactory.GetRepository();
                    var transmissionsRepo = TransmissionFactoryRepository.GetRepository();
                    var colorsRepo = ColorsRepositoryFactory.GetRepository();
                    var interiorsRepo = InteriorsRepositoryFactory.GetRepository();

                    model.Vehicle = new Vehicle();
                    model.Makes = new SelectList(makesRepo.GetAll(), "MakeId", "MakeName");
                    model.VehicleTypes = new SelectList(vehicleTypesRepo.GetAll(), "VehicleTypeId", "VehicleTypeName");
                    model.BodyStyles = new SelectList(bodyStylesRepo.GetAll(), "BodyStyleId", "BodyStyleType");
                    model.Transmissions = new SelectList(transmissionsRepo.GetAll(), "TransmissionId", "TransmissionType");
                    model.Colors = new SelectList(colorsRepo.GetAll(), "ColorId", "ColorName");
                    model.Interiors = new SelectList(interiorsRepo.GetAll(), "InteriorId", "InteriorType");

                    return View(model);
                }

            }
            else
            {
                var vehicleRepo = VehicleRepositoryFactory.GetRepository();
                var makesRepo = MakeRepositoryFactory.GetRepository();
                var vehicleTypesRepo = VehicleTypeRepositoryFactory.GetRepository();
                var bodyStylesRepo = BodyStyleRepositoryFactory.GetRepository();
                var transmissionsRepo = TransmissionFactoryRepository.GetRepository();
                var colorsRepo = ColorsRepositoryFactory.GetRepository();
                var interiorsRepo = InteriorsRepositoryFactory.GetRepository();

                model.Vehicle = new Vehicle();
                model.Makes = new SelectList(makesRepo.GetAll(), "MakeId", "MakeName");
                model.VehicleTypes = new SelectList(vehicleTypesRepo.GetAll(), "VehicleTypeId", "VehicleTypeName");
                model.BodyStyles = new SelectList(bodyStylesRepo.GetAll(), "BodyStyleId", "BodyStyleType");
                model.Transmissions = new SelectList(transmissionsRepo.GetAll(), "TransmissionId", "TransmissionType");
                model.Colors = new SelectList(colorsRepo.GetAll(), "ColorId", "ColorName");
                model.Interiors = new SelectList(interiorsRepo.GetAll(), "InteriorId", "InteriorType");

                return View(model);
            }
        }

        [HttpGet]
        public ActionResult EditVehicle(int id)
        {
            var model = new VehicleEditVM();

            var vehicleRepo = VehicleRepositoryFactory.GetRepository();
            var modelsRepo = ModelRepositoryFactory.GetRepository();
            var makesRepo = MakeRepositoryFactory.GetRepository();
            var vehicleTypesRepo = VehicleTypeRepositoryFactory.GetRepository();
            var bodyStylesRepo = BodyStyleRepositoryFactory.GetRepository();
            var transmissionsRepo = TransmissionFactoryRepository.GetRepository();
            var colorsRepo = ColorsRepositoryFactory.GetRepository();
            var interiorsRepo = InteriorsRepositoryFactory.GetRepository();

            model.Vehicle = vehicleRepo.GetById(id);

            model.MakeId = modelsRepo.GetById(model.Vehicle.ModelId).MakeId;

            model.Models = new SelectList(modelsRepo.GetModelsByMakeId(model.MakeId).ToList(), "ModelId", "ModelName");
            model.Makes = new SelectList(makesRepo.GetAll(), "MakeId", "MakeName");
            model.VehicleTypes = new SelectList(vehicleTypesRepo.GetAll(), "VehicleTypeId", "VehicleTypeName");
            model.BodyStyles = new SelectList(bodyStylesRepo.GetAll(), "BodyStyleId", "BodyStyleType");
            model.Transmissions = new SelectList(transmissionsRepo.GetAll(), "TransmissionId", "TransmissionType");
            model.Colors = new SelectList(colorsRepo.GetAll(), "ColorId", "ColorName");
            model.Interiors = new SelectList(interiorsRepo.GetAll(), "InteriorId", "InteriorType");

            return View(model);
        }

        [HttpPost]
        public ActionResult EditVehicle(VehicleEditVM model)
        {
            if (ModelState.IsValid)
            {
                if (model.VinUniqueFlag)
                {
                    var vehicleRepo = VehicleRepositoryFactory.GetRepository();

                    try
                    {
                        if (model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
                        {
                            var savepath = Server.MapPath("~/Images");

                            string fileName = "inventory-" + model.Vehicle.Vin;
                            string extension = Path.GetExtension(model.ImageUpload.FileName);

                            var filePath = Path.Combine(savepath, fileName + extension);

                            if (System.IO.File.Exists(filePath))
                            {
                                System.IO.File.Delete(filePath);
                            }

                            model.ImageUpload.SaveAs(filePath);

                            model.Vehicle.ImageFileName = Path.GetFileName(filePath);
                        }
                        else
                        {
                            model.Vehicle.ImageFileName = vehicleRepo.GetById(model.Vehicle.VehicleId).ImageFileName;
                        }

                        vehicleRepo.Update(model.Vehicle);

                        TempData["UserMessage"] = "Vehicle Successfully Updated!";

                        return RedirectToAction("EditVehicle", new { @id = model.Vehicle.VehicleId });
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    TempData["UserMessage"] = "Vehicle Failed to Update: Unique VIN # Constraint";

                    var vehicleRepo = VehicleRepositoryFactory.GetRepository();
                    var modelsRepo = ModelRepositoryFactory.GetRepository();
                    var makesRepo = MakeRepositoryFactory.GetRepository();
                    var vehicleTypesRepo = VehicleTypeRepositoryFactory.GetRepository();
                    var bodyStylesRepo = BodyStyleRepositoryFactory.GetRepository();
                    var transmissionsRepo = TransmissionFactoryRepository.GetRepository();
                    var colorsRepo = ColorsRepositoryFactory.GetRepository();
                    var interiorsRepo = InteriorsRepositoryFactory.GetRepository();

                    model.Vehicle = vehicleRepo.GetById(model.Vehicle.VehicleId);

                    model.MakeId = modelsRepo.GetAll().Where(m => m.ModelId == model.Vehicle.ModelId).FirstOrDefault().MakeId;

                    model.Models = new SelectList(modelsRepo.GetModelsByMakeId(model.MakeId).ToList(), "ModelId", "ModelName");
                    model.Makes = new SelectList(makesRepo.GetAll(), "MakeId", "MakeName");
                    model.VehicleTypes = new SelectList(vehicleTypesRepo.GetAll(), "VehicleTypeId", "VehicleTypeName");
                    model.BodyStyles = new SelectList(bodyStylesRepo.GetAll(), "BodyStyleId", "BodyStyleType");
                    model.Transmissions = new SelectList(transmissionsRepo.GetAll(), "TransmissionId", "TransmissionType");
                    model.Colors = new SelectList(colorsRepo.GetAll(), "ColorId", "ColorName");
                    model.Interiors = new SelectList(interiorsRepo.GetAll(), "InteriorId", "InteriorType");

                    return View(model);
                }
            }
            else
            {
                var vehicleRepo = VehicleRepositoryFactory.GetRepository();
                var modelsRepo = ModelRepositoryFactory.GetRepository();
                var makesRepo = MakeRepositoryFactory.GetRepository();
                var vehicleTypesRepo = VehicleTypeRepositoryFactory.GetRepository();
                var bodyStylesRepo = BodyStyleRepositoryFactory.GetRepository();
                var transmissionsRepo = TransmissionFactoryRepository.GetRepository();
                var colorsRepo = ColorsRepositoryFactory.GetRepository();
                var interiorsRepo = InteriorsRepositoryFactory.GetRepository();

                model.Vehicle = vehicleRepo.GetById(model.Vehicle.VehicleId);

                model.MakeId = modelsRepo.GetAll().Where(m => m.ModelId == model.Vehicle.ModelId).FirstOrDefault().MakeId;

                model.Models = new SelectList(modelsRepo.GetModelsByMakeId(model.MakeId).ToList(), "ModelId", "ModelName");
                model.Makes = new SelectList(makesRepo.GetAll(), "MakeId", "MakeName");
                model.VehicleTypes = new SelectList(vehicleTypesRepo.GetAll(), "VehicleTypeId", "VehicleTypeName");
                model.BodyStyles = new SelectList(bodyStylesRepo.GetAll(), "BodyStyleId", "BodyStyleType");
                model.Transmissions = new SelectList(transmissionsRepo.GetAll(), "TransmissionId", "TransmissionType");
                model.Colors = new SelectList(colorsRepo.GetAll(), "ColorId", "ColorName");
                model.Interiors = new SelectList(interiorsRepo.GetAll(), "InteriorId", "InteriorType");

                return View(model);
            }
        }

        [HttpPost]
        public ActionResult DeleteVehicle(int id)
        {
            var vehiclerepo = VehicleRepositoryFactory.GetRepository();

            try
            {
                if (!string.IsNullOrEmpty(vehiclerepo.GetById(id).ImageFileName))
                {
                    var savepath = Server.MapPath("~/Images");
                    string fileName = vehiclerepo.GetById(id).ImageFileName;
                    var filePath = Path.Combine(savepath, fileName);

                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                vehiclerepo.Delete(id);

                return RedirectToAction("Index", "Admin");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult Users()
        {
            var userMgr = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var model = new UsersVM();

            model.Users = userMgr.Users.ToList();
            model.Roles = new Dictionary<string, string>();

            foreach (var user in model.Users)
            {
                var roles = userMgr.GetRoles(user.Id).ToList();

                if (roles.Any())
                {
                    var userRole = string.Join(", ", roles);
                    model.Roles.Add(user.Id, userRole);
                }
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Makes()
        {
            var model = new MakesVM();

            var makeRepo = MakeRepositoryFactory.GetRepository();

            model.Makes = makeRepo.GetAll();
            model.Make = new Make();

            return View(model);
        }

        [HttpPost]
        public ActionResult Makes(MakesVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var makeRepo = MakeRepositoryFactory.GetRepository();

                    model.Make.DateAdded = DateTime.Today;
                    model.Make.EmployeeEmail = User.Identity.GetUserName();

                    makeRepo.Insert(model.Make);

                    model.Makes = makeRepo.GetAll();

                    return RedirectToAction("Makes");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                var makeRepo = MakeRepositoryFactory.GetRepository();

                model.Makes = makeRepo.GetAll();
                model.Make = new Make();

                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Models()
        {
            var model = new ModelsVM();

            var modelRepo = ModelRepositoryFactory.GetRepository();
            var makeRepo = MakeRepositoryFactory.GetRepository();

            model.NewModel = new Model();
            model.Models = modelRepo.GetAll();
            model.Makes = makeRepo.GetAll();

            return View(model);
        }
        [HttpPost]
        public ActionResult Models(ModelsVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var modelRepo = ModelRepositoryFactory.GetRepository();
                    var makeRepo = MakeRepositoryFactory.GetRepository();

                    model.NewModel.DateAdded = DateTime.Today;
                    model.NewModel.EmployeeEmail = User.Identity.GetUserName();

                    modelRepo.Insert(model.NewModel);

                    model.Models = modelRepo.GetAll();
                    model.Makes = makeRepo.GetAll();

                    return RedirectToAction("Models");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                var modelRepo = ModelRepositoryFactory.GetRepository();
                var makeRepo = MakeRepositoryFactory.GetRepository();

                model.Models = modelRepo.GetAll();
                model.Makes = makeRepo.GetAll();
                model.NewModel = new Model();

                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Specials()
        {
            var model = new SpecialsVM();

            var specialRepo = SpecialRepositoryFactory.GetRepository();

            model.Specials = specialRepo.GetAll();
            model.Special = new Special();

            return View(model);
        }
        [HttpPost]
        public ActionResult Specials(SpecialsVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var specialRepo = SpecialRepositoryFactory.GetRepository();

                    specialRepo.Insert(model.Special);

                    TempData["UserMessages"] = "Special successfully added!";
                    return RedirectToAction("Specials");
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                var specialRepo = SpecialRepositoryFactory.GetRepository();

                model.Specials = specialRepo.GetAll();
                model.Special = new Special();
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult DeleteSpecial(int specialId)
        {
            try
            {
                var repo = SpecialRepositoryFactory.GetRepository();

                repo.Delete(specialId);

                return RedirectToAction("Specials", "Admin");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}