using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class UsersVM
    {
        public IEnumerable<ApplicationUser> Users { get; set; }
        public Dictionary<string, string> Roles { get; set; }
    }
}