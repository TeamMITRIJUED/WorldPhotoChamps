namespace Champ.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<PhotoContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(PhotoContext context)
        {
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                roleManager.Create(new IdentityRole("Admin"));
            }

            if (!context.Users.Any(u => u.UserName == resources.AdminName))
            {
                var userManager = new UserManager<User>(new UserStore<User>(context));
                var admin = new User
                {
                    UserName = resources.AdminName,
                    Email = resources.AdminEmail
                };

                userManager.Create(admin, resources.AdminPassword);
                userManager.AddToRole(admin.Id, "Admin");
            }
        }
    }
}
