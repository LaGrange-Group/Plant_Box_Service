using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plant_Box_Service.Models
{
    public class AdminViewModel
    {
        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<Payment> Payments { get; set; }
        public IEnumerable<Gift> Gifts { get; set; }
        public IEnumerable<Shipment> Shipments { get; set; }
        public Preference Preference { get; set; }
        public Customer Customer { get; set; }
        public Shipment Shipment { get; set; }
    }
}