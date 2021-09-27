using System;
using System.Collections.Generic;
using System.Text;

namespace GuildCars.Models.Tables
{
    public class Make
    { 
        public int MakeId { get; set; }
        public string MakeName { get; set; }
        public DateTime DateAdded { get; set; }
        public string EmployeeEmail { get; set; }
    }
}
