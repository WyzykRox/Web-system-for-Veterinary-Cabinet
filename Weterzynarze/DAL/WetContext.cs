using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Weterzynarze.ViewModels;

namespace Weterzynarze.DAL
{ 
    public class WetContext : DbContext
    {
        public  WetContext() : base("DefaultConnection")
        {

        }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Animal> Animals { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
           
        }

        public System.Data.Entity.DbSet<Weterzynarze.viewModels.Image> Images { get; set; }
    }
}
