using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foroffer.Models
{
    public class Webstat
    {
        public int Id { get; set; }
        public string Page { get; set; }
        public DateTime VisitDate { get; set; }
        public int VisitDay { get; set; }
        public int AsOfDate { get; set; }
        public int Daily { get; set; }
        public int VisitMonth { get; set; }
        public int VisitYear { get; set; }
    }
}
