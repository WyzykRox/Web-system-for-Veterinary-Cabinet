using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Weterzynarze.Models;

namespace Weterzynarze.ViewModels
{
    public class Profile
    {
        [Key]
        public int ID { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }


        public virtual List<Visit> Zapiswiz { get; set; }
        public DateTime Created { get; } = DateTime.Now;
        public virtual List<Animal> Pets { get; set; }
      

        public Profile()
        {

        }
        public Profile(string email)
        {
            Email = email;
        }
    }
}