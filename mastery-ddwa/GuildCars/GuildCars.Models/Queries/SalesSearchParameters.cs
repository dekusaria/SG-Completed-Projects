using System;
using System.Collections.Generic;
using System.Text;

namespace GuildCars.Models.Queries
{
    public class SalesSearchParameters
    {
        public string UserEmail { get; set; }
        public DateTime? MinDate { get; set; }
        public DateTime? MaxDate { get; set; }
    }
}
