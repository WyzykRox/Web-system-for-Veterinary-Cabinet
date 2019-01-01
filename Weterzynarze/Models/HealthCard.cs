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
        public DateTime CreateTime { get; set; }
        public string Description { get; set; }
        public string Treatment { get; set; }
        public int AnimalID { get; set; }
       
        public virtual Animal Animal { get; set; }
    }
}