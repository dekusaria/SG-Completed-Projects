using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildCars.Models.Queries
{
    public class PurchaseItem
    {
        public string Vin { get; set; }
        public Customer Customer { get; set; }
        public Purchase Purchase { get; set; }
    }
}
