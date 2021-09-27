using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class MakesVM : IValidatableObject
    {
        public Make Make { get; set; }
        public IEnumerable<Make> Makes { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (string.IsNullOrEmpty(Make.MakeName))
            {
                errors.Add(new ValidationResult("Make Name is required"));
            }

            return errors;
        }
    }
}