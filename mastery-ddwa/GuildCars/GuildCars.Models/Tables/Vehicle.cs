using System;
using System.Collections.Generic;
using System.Text;

namespace GuildCars.Models.Tables
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public string Vin { get; set; }
        public int VehicleTypeId { get; set; }
        public int ModelId { get; set; }
        public int ColorId { get; set; }
        public int InteriorId { get; set; }
        public int BodyStyleId { get; set; }
        public int TransmissionId { get; set; }
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
