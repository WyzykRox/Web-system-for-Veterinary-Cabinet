using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Weterzynarze.ViewModels;

namespace Weterzynarze.viewModels
{
    public class Image
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int PetId { get; set; }

        public virtual Animal PetImage { get; set; }


    }
}