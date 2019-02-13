using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Weterzynarze.Models;
using Weterzynarze.ViewModels;

namespace Weterzynarze.DAL
{ 
    public class WetContext : DbContext
    {
        public  WetContext() : base("DefaultConnection")
        {

        }
        public DbSet<Service> Services { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<HealthCard> HealthCards { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<HistryVisit> HistoryVisits { get; set; }
        public DbSet<FilesHealthCard> Files { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}
