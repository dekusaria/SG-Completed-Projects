using System;
using System.Collections.Generic;
using System.Text;

namespace GuildCars.Models.Queries
{
    public class VehicleItem
    {
        public int VehicleId { get; set; }
        public string Vin { get; set; }
        public string VehicleTypeName { get; set; }
        public string ModelName { get; set; }
        public string MakeName { get; set; }
        public string ColorName { get; set; }
        public string InteriorType { get; set; }
        public string BodyStyleType { get; set; }
        public string TransmissionType { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
        public decimal Msrp { get; set; }
        public decimal SalePrice { get; set; }
        public string Description { get; set; }
        public string ImageFileName { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsSold { get; set; }
    }
}
