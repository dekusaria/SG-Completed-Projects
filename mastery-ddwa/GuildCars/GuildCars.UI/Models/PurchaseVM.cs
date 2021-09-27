using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Models
{
    public class PurchaseVM : IValidatableObject
    {
        public IEnumerable<SelectListItem> States { get; set; }
        public IEnumerable<SelectListItem> PurchaseTypes { get; set; }
        public VehicleItem Vehicle { get; set; }
        public Customer Customer { get; set; }
        public Address Address { get; set; }
        public Purchase Purchase { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (string.IsNullOrEmpty(Customer.CustomerFirstName))
                errors.Add(new ValidationResult("Customer's First Name is required"));
            
            if (string.IsNullOrEmpty(Customer.CustomerLastName))
                errors.Add(new ValidationResult("Customer's Last Name is required"));

            if (string.IsNullOrEmpty(Customer.CustomerPhone) && string.IsNullOrEmpty(Customer.CustomerEmail))
                errors.Add(new ValidationResult("You must enter either the customer's Phone Number or their Email"));

            Regex phone = new Regex(@"^[1-9]\d{2}-\d{3}-\d{4}");

            if (Customer.CustomerPhone != null && !phone.IsMatch(Customer.CustomerPhone))
                errors.Add(new ValidationResult("Phone number must match format XXX-XXX-XXXX"));

            if (string.IsNullOrEmpty(Address.Street1))
                errors.Add(new ValidationResult("Street 1 is required"));

            if (string.IsNullOrEmpty(Address.City))
                errors.Add(new ValidationResult("City is required"));

            if (string.IsNullOrEmpty(Address.Zipcode))
                errors.Add(new ValidationResult("Zipcode is required"));

            if (!string.IsNullOrEmpty(Address.Zipcode) && Address.Zipcode.Length != 5)
                errors.Add(new ValidationResult("Zipcode must be 5 digits"));

            if (Address.StateId == null)
                errors.Add(new ValidationResult("State is required"));

            if (Purchase.PurchasePrice == 0)
                errors.Add(new ValidationResult("Purchase Price is required"));

            if (Purchase.PurchasePrice < (Vehicle.SalePrice * 0.95M))
                errors.Add(new ValidationResult("Purchase Price cannot be less than 95% of the sales price"));

            if (Purchase.PurchasePrice > Vehicle.Msrp)
                errors.Add(new ValidationResult("Purchase Price cannot exceed the MSRP"));

            return errors;
        }
    }
}