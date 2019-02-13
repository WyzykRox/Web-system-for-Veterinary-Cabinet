using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Weterzynarze.Models
{
    public class FilesHealthCard
    {
        [Key]
        public int ID { get; set; }
        public int KartaID { get; set; }
        public string Src { get; set; }
        public string Name { get; set; }
        public virtual HealthCard Karta { get; set; }
    }
}