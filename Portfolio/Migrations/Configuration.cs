namespace Portfolio.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Portfolio.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Portfolio.Models.ApplicationDbContext context)
        {
            
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }
            if (!context.Roles.Any(r => r.Name == "Moderator"))
            {
                var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));
                roleManager.Create(new IdentityRole { Name = "Moderator" });
            }

            if (!context.Users.Any(u => u.Email == "mrclutts@ncsu.edu"))
            {
                var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
                userManager.Create(new ApplicationUser
                {
                    UserName = "mrclutts@ncsu.edu",
                    Email = "mrclutts@ncsu.edu",
                    FirstName = "Matt",
                    LastName = "Clutts",
                    DisplayName = "Matt"
                }, "Potato99!");

                var userId = userManager.FindByEmail("mrclutts@ncsu.edu").Id;
                userManager.AddToRole(userId, "Admin");
            }
            if (!context.Users.Any(u => u.Email == "moderator@coderfoundry.com"))
            {
                var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
                userManager.Create(new ApplicationUser
                {
                    UserName = "moderator@coderfoundry.com",
                    Email = "moderator@coderfoundry.com",
                    DisplayName = "Ria & Antonio"
                }, "Password-1");
            }
            else { 

                var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
                var userId = userManager.FindByEmail("moderator@coderfoundry.com").Id;
                userManager.AddToRole(userId, "Moderator");
            }
        }
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
