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
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Imię")]
        public string FirstName { get; set; }
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }
        [Display(Name = "numer telefonu")]
        public string PhoneNumber { get; set; }


        public virtual List<Visit> Zapiswiz { get; set; }
        public DateTime Created { get; } = DateTime.Now;
        public virtual List<Animal> Pets { get; set; }
        public virtual List<News> post { get; set; }

        public Profile()
        {

        }
        public Profile(string email)
        {
            Email = email;
        }
    }
}