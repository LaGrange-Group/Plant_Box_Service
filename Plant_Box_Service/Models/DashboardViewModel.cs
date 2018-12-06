using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plant_Box_Service.Models
{
    public class DashboardViewModel
    {
        public Customer Customer { get; set; }
        public Preference Preference { get; set; }
        public Payment Payment { get; set; }
        public IEnumerable<Payment> Payments { get; set; }
    }
}