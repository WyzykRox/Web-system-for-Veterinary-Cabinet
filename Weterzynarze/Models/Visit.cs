﻿using System;
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
        public DateTime VisitDate { get; set; }
        public string Description { get; set; }

        public virtual Profile User { get; set; }
        public virtual HealthCard Wpis { get; set; }
    }
}