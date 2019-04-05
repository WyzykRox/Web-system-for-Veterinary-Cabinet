using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using Weterzynarze.Models;
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
            //utworzenie ról
            roleManager.Create(new IdentityRole("Admin"));
            roleManager.Create(new IdentityRole("User"));


            var user = new ApplicationUser { UserName = "janusz@janusz.com", Email = "janusz@janusz.com" };
            string pass = "janusz";
            userManager.Create(user, pass);
            userManager.AddToRole(user.Id, "Admin");
            userManager.AddToRole(user.Id, "User");

            var user2 = new ApplicationUser { UserName = "grazyna@grazyna.com", Email = "grazyna@grazyna.com" };
            string pass2 = "grazyna";
            userManager.Create(user2, pass2);
            userManager.AddToRole(user.Id, "User");

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

            var race = new List<Race>
            {
                new Race
                {
                    Name = "Pers",
                },
                new Race
                {
                    Name = "Husky",
                },
                new Race
                {
                    Name = "Bulldog",
                },
                new Race
                {
                    Name = "Papuga",
                },
                new Race
                {
                    Name = "Tarantula",
                },
                new Race
                {
                    Name = "goldenretriver",
                }
            };

            race.ForEach(p => context.Races.Add(p));
            context.SaveChanges();

            var service = new List<Service>
            {
                new Service
                {
                    Name = "Odrobaczanie",
                    Price = 40,
                },
                new Service
                {
                    Name = "Szczepienie",
                    Price = 40,
                },
                new Service
                {
                    Name = "Diagnozowanie choroby",
                    Price = 40,
                },
                new Service
                {
                    Name = "strzyżenie",
                    Price = 40,
                }
            };
            service.ForEach(p => context.Services.Add(p));
            context.SaveChanges();

        }
    }
}