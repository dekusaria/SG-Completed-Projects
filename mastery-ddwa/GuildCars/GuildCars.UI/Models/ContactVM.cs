using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class ContactVM : IValidatableObject
    {
        public string Vin { get; set; }
        public Contact Contact { get; set; }
        public Customer Customer { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (string.IsNullOrEmpty(Customer.CustomerFirstName))
                errors.Add(new ValidationResult("First Name is required."));

            if (string.IsNullOrEmpty(Customer.CustomerLastName))
                errors.Add(new ValidationResult("Last Name is required."));

            if (string.IsNullOrEmpty(Contact.ContactMessage))
                errors.Add(new ValidationResult("Message is required."));

            if (string.IsNullOrEmpty(Customer.CustomerEmail) && string.IsNullOrEmpty(Customer.CustomerPhone))
                errors.Add(new ValidationResult("You must enter either a Phone Number or a valid Email."));

            if (Customer.CustomerPhone != null && Customer.CustomerPhone.Length > 12)
                errors.Add(new ValidationResult("Phone Number is too long. Recommended format: XXX-XXX-XXXX"));

            return errors;
        }
    }
}