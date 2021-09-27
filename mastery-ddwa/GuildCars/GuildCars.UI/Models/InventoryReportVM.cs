using GuildCars.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class InventoryReportVM
    {
        public IEnumerable<InventoryReport> NewInventory { get; set; }
        public IEnumerable<InventoryReport> UsedInventory { get; set; }
    }
}