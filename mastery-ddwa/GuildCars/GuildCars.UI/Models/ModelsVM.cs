using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class ModelsVM : IValidatableObject
    {
        public Model NewModel { get; set; }
        public IEnumerable<Make> Makes { get; set; }
        public IEnumerable<Model> Models { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (string.IsNullOrEmpty(NewModel.ModelName))
            {
                errors.Add(new ValidationResult("Make Name is required"));
            }

            if (NewModel.MakeId == 0)
            {
                errors.Add(new ValidationResult("You must select a Make"));
            }

            return errors;
        }
    }
}