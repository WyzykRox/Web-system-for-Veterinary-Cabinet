using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Weterzynarze.viewModels;

namespace Weterzynarze.ViewModels
{
    public class Animal
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string HairColor { get; set; }
        public string Picture { get; set; }
        public DateTime Created { get; } = DateTime.Now;

        public virtual Profile Owner { get; set; }

    }
}