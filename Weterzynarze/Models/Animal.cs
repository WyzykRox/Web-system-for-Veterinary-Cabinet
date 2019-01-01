using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Weterzynarze.Models;

namespace Weterzynarze.ViewModels
{
    public class Animal
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Colour { get; set; }
        public string Sex { get; set; }
        public string Breed { get; set; }
        public string DistinuishingMarks { get; set; }
        public int ChipId { get; set; }
        public string Picture { get; set; }
        public DateTime Created { get; set; }

        public virtual Profile Owner { get; set; }
        public virtual List<HealthCard> HealthCard { get; set; }
    }
}