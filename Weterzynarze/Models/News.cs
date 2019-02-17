using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Weterzynarze.ViewModels;

namespace Weterzynarze.Models
{
    public class News
    {
        public int ID { get; set; }
        [Display(Name = "Tytuł")]
        public string Title { get; set; }
        [Display(Name = "Treść")]
        public string Synopsis { get; set; }
        [Display(Name = "Czas utworzenia")]
        public DateTime CreateTime { get; } = DateTime.Now;
        public int UserID { get; set; }

        public virtual Profile User { get; set; }
    }
}