using System;
using System.Collections.Generic;
using System.Text;

namespace GuildCars.Models.Queries
{
    public class SalesReport
    {
        public string UserName { get; set; }
        public decimal TotalSales { get; set; }
        public int TotalVehicles { get; set; }
    }
}
