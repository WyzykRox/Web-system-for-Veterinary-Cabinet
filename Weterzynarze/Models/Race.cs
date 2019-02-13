using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Weterzynarze.ViewModels;

namespace Weterzynarze.Models
{
    public class Race
    {
        public int ID { get; set; }
        [Display(Name="Rasa")]
        public string Name { get; set; }

       

    }
}