using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Weterzynarze.ViewModels;

namespace Weterzynarze.Models
{
    public class HealthCard
    {
        [Key]
        public int ID { get; set; }
        [Display(Name ="Data utworzenia")]
        public DateTime CreateTime { get; set; }
        [Display(Name = "Opis")]
        public string Description { get; set; }
        [Display(Name = "Leczenie")]
        public string Treatment { get; set; }
        [Display(Name = "nazwa zwierzaka")]
        public int AnimalID { get; set; }
        [Display(Name = "Usługa")]
        public int uslugaID { get; set; }

        public virtual Animal Animal { get; set; }
      
        public virtual Service usluga { get; set; }
        [Display(Name = "Lista Plików")]
        public virtual List<FilesHealthCard> FilesSrcList { get; set; }
    }
}