using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class SpecialsVM : IValidatableObject
    {
        public Special Special { get; set; }
        public IEnumerable<Special> Specials { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (string.IsNullOrEmpty(Special.SpecialTitle))
            {
                errors.Add(new ValidationResult("Special Title is required"));
            }
            else if (Special.SpecialTitle.Length > 50)
            {
                errors.Add(new ValidationResult("Special Title cannot exceed 50 characters in length"));
            }

            if (string.IsNullOrEmpty(Special.SpecialDescription))
            {
                errors.Add(new ValidationResult("Special Description is required"));
            }
            else if (Special.SpecialDescription.Length > 500)
            {
                errors.Add(new ValidationResult("Special Description cannot exceed 500 characters in length"));
            }

            return errors;
        }
    }
}