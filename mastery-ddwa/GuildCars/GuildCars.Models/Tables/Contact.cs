using System;
using System.Collections.Generic;
using System.Text;

namespace GuildCars.Models.Tables
{
    public class Contact
    {
        public int ContactId { get; set; }
        public int CustomerId { get; set; }
        public string ContactMessage { get; set; }
    }
}
