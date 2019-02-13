using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Weterzynarze.Models
{
    public class Service
    {
        public int ID { get; set; }
        [Display(Name = "nazwa usługi")]
        public string Name { get; set; }
        [Display(Name = "Cena")]
        public int Price { get; set; }
    }
}