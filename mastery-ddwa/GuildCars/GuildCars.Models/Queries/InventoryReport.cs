using System;
using System.Collections.Generic;
using System.Text;

namespace GuildCars.Models.Queries
{
    public class InventoryReport
    {
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Count { get; set; }
        public decimal StockValue { get; set; }
    }
}
