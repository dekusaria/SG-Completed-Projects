using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Models
{
    public class VehicleEditVM : IValidatableObject
    {
        public Vehicle Vehicle { get; set; }
        public int MakeId { get; set; }
        public HttpPostedFileBase ImageUpload { get; set; }
        public IEnumerable<SelectListItem> Makes { get; set; }
        public IEnumerable<SelectListItem> Models { get; set; }
        public IEnumerable<SelectListItem> VehicleTypes { get; set; }
        public IEnumerable<SelectListItem> BodyStyles { get; set; }
        public IEnumerable<SelectListItem> Transmissions { get; set; }
        public IEnumerable<SelectListItem> Colors { get; set; }
        public IEnumerable<SelectListItem> Interiors { get; set; }
        public bool VinUniqueFlag { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            Regex regex = new Regex(@"[^A - Za - z0 - 9] +");

            if (Vehicle.Year < 2000 || Vehicle.Year > DateTime.Today.Year + 1)
                errors.Add(new ValidationResult($"Vehicle Year must be between 2000 and {DateTime.Today.Year + 1}"));

            if (string.IsNullOrEmpty(Vehicle.Vin))
            {
                errors.Add(new ValidationResult("Please enter a valid VIN #"));
            }
            else if (Vehicle.Vin.Length != 16)
            {
                errors.Add(new ValidationResult("VIN # must be 16 characters long"));
            }
            else if (regex.IsMatch(Vehicle.Vin))
            {
                errors.Add(new ValidationResult("VIN # can only contain letters A-Z and numbers 0-9"));
            }

            if (Vehicle.ModelId == 0)
                errors.Add(new ValidationResult("Model is required"));

            if (Vehicle.Mileage < 0)
            {
                errors.Add(new ValidationResult("Mileage cannot be lower than 0"));
            }
            else if (Vehicle.VehicleTypeId == 1 && Vehicle.Mileage > 1000)
            {
                errors.Add(new ValidationResult("New vehicles must have a mileage less than or equal to 1000"));
            }
            else if (Vehicle.VehicleTypeId == 2 && Vehicle.Mileage <= 1000)
            {
                errors.Add(new ValidationResult("Used vehicles must have a mileage greater than 1000"));
            }

            if (Vehicle.SalePrice < 0 || Vehicle.Msrp < 0)
            {
                errors.Add(new ValidationResult("Sale Price and MSRP must be positive numbers"));
            }
            else if (Vehicle.SalePrice > Vehicle.Msrp)
            {
                errors.Add(new ValidationResult("Sale Price must be less than MSRP"));
            }

            if (string.IsNullOrEmpty(Vehicle.Description))
            {
                errors.Add(new ValidationResult("Description is required"));
            }
            else if (Vehicle.Description.Length > 200)
            {
                errors.Add(new ValidationResult("Description cannot be longer than 200 characters"));
            }

            if (ImageUpload != null && ImageUpload.ContentLength > 0)
            {
                var extensions = new string[] { ".jpg", ".png", ".jpeg" };

                var extension = Path.GetExtension(ImageUpload.FileName);

                if (!extensions.Contains(extension))
                {
                    errors.Add(new ValidationResult("Image file must be a jpg, png, or jpeg."));
                }
            }

            return errors;
        }
    }
}