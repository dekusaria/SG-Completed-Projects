using System;
using System.Collections.Generic;
using System.Text;

namespace GuildCars.Models.Queries
{
    public class VehicleShortItem
    {
        public int VehicleId { get; set; }
        public string Vin { get; set; }
        public int Year { get; set; }
        public string MakeName { get; set; }
        public string ModelName { get; set; }
        public decimal SalePrice { get; set; }
        public string ImageFileName { get; set; }
    }
}
