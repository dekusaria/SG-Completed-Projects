namespace GuildCars.UI.Migrations
{
    using GuildCars.UI.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GuildCars.UI.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GuildCars.UI.Models.ApplicationDbContext context)
        {

            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleMgr = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(context));

            if (roleMgr.RoleExists("admin") && roleMgr.RoleExists("sales") && roleMgr.RoleExists("disabled"))
                return;

            roleMgr.Create(new ApplicationRole() { Name = "admin" });
            roleMgr.Create(new ApplicationRole() { Name = "sales" });
            roleMgr.Create(new ApplicationRole() { Name = "disabled" });

            var user = new ApplicationUser() { UserName = "admin@email.com", Email = "admin@email.com", FirstName = "admin", LastName = "administrator" };

            userMgr.Create(user, "Testing123!");

            userMgr.AddToRole(user.Id, "admin");


            var salesUser = new ApplicationUser() { UserName = "sales@email.com", Email = "sales@email.com", FirstName = "sales", LastName = "salesperson" };

            userMgr.Create(salesUser, "Sales123!");

            userMgr.AddToRole(salesUser.Id, "sales");

            var disabledUser = new ApplicationUser() { UserName = "disabled@email.com", Email = "disabled@email.com", FirstName = "disabled", LastName = "bannished" };

            userMgr.Create(disabledUser, "Disabled123!");

            userMgr.AddToRole(disabledUser.Id, "disabled");
        }
    }
}
