using System;
using System.Collections.Generic;
using System.Text;

namespace GuildCars.Models.Tables
{
    public class Special
    {
        public int SpecialId { get; set; }
        public int? VehicleId { get; set; }
        public string SpecialTitle { get; set; }
        public string SpecialDescription { get; set; }
    }
}
