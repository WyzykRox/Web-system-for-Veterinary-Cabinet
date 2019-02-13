using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Weterzynarze.ViewModels;

namespace Weterzynarze.Models
{
    public class HistryVisit
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Data wizyty")]
        public DateTime VisitDate { get; set; }
        [Display(Name = "opis dolegliwości")]
        public string Description { get; set; }
        [Display(Name = "Uwagi z wizyty")]
        public string Attention { get; set; }
        [Display(Name = "nazwa zwierzaka")]
        public int AnimalID { get; set; }


        public virtual Profile User { get; set; }
        public virtual Animal Zwierzak { get; set; }
    }
}