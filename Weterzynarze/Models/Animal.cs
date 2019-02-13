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
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Display(Name = "Maść sierści")]
        public string Colour { get; set; }
        [Display(Name = "Płeć")]
        public string Sex { get; set; }
        [Display(Name = "Znaki szczególne")]
        public string DistinuishingMarks { get; set; }
        [Display(Name = "Numer czipu")]
        public int ChipId { get; set; }
        [Display(Name = "Zdjęcie")]
        public string Picture { get; set; }
        [Display(Name ="Rasa")]
        public int RaceID { get; set; }
        [Display(Name = "Data narodzin")]
        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }
        [Display(Name = "Data ostatniego szczepienia")]
        public DateTime grafting { get; } = DateTime.Now;


        public virtual Race Rasa { get; set; }
        public virtual Profile Owner { get; set; }
        public virtual List<HealthCard> HealthCard { get; set; }
    }
}