using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using Weterzynarze.ViewModels;

namespace Weterzynarze.DAL
{
    public class DataInitializer : DropCreateDatabaseIfModelChanges<WetContext>
    {
        protected override void Seed(WetContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(new ApplicationDbContext()));

            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(new ApplicationDbContext()));

            roleManager.Create(new IdentityRole("Admin"));
            roleManager.Create(new IdentityRole("User"));
            roleManager.Create(new IdentityRole("Moderator"));

           

            var user = new ApplicationUser { UserName = "janusz@janusz.com" };
            string pass = "janusz";
            userManager.Create(user, pass);
            userManager.AddToRole(user.Id, "Admin");

            var user2 = new ApplicationUser { UserName = "grazyna@grazyna.com" };
            string pass2 = "grazyna";
            userManager.Create(user2, pass2);
            userManager.AddToRole(user.Id, "Admin");

            var profiles = new List<Profile>
            {
                new Profile
                {
                    Email = "janusz@janusz.com",
                    FirstName ="Grażyna",
                    LastName = "Kowalska",
                },
                new Profile
                {
                    Email = "grazyna@grazyna.com",
                    FirstName ="Grażyna",
                    LastName = "Kowalska",
                }
            };

            profiles.ForEach(p => context.Profiles.Add(p));
            context.SaveChanges();


        }
    }
}