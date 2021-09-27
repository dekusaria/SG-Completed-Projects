using System;
using System.Collections.Generic;
using System.Text;

namespace GuildCars.Models.Tables
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public int? AddressId { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
    }
}
