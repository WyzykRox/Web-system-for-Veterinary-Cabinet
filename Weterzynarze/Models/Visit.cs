using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Weterzynarze.ViewModels;

namespace Weterzynarze.Models
{
    public class Visit
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Data Wizyt")]
        public DateTime VisitDate { get; set; }
        [Display(Name = "Opis dolegliwości")]
        public string Description { get; set; }
        [Display(Name = "wybierz ziwerzaka")]
        public int AnimalID { get; set; }


        public virtual Profile User { get; set; }
        public virtual Animal Zwierzak { get; set; }
    }
}