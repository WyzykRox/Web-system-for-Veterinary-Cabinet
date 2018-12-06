using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weterzynarze.Models
{
    public class HistoryVisit
    {
        public int ID { get; set; }
        public DateTime VisitDate { get; set; }
        public Boolean TookPlace { get; set; }

    }
}