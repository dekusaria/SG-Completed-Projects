using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class HomeVM
    {
        public IEnumerable<VehicleShortItem> Featured { get; set; }
        public IEnumerable<Special> Specials { get; set; }
    }
}