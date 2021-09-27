using System;
using System.Collections.Generic;
using System.Text;

namespace GuildCars.Models.Tables
{
    public class Purchase
    {
        public int PurchaseId { get; set; }
        public int CustomerId { get; set; }
        public int PurchaseTypeId { get; set; }
        public int VehicleId { get; set; }
        public decimal PurchasePrice { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string SoldByEmail { get; set; }
    }
}
