using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Plant_Box_Service.Models
{
    public class Shipment
    {
        [Key]
        public int Id { get; set; }
        public string TrackingHash { get; set; }
        public bool Status { get; set; }
        public bool Delivered { get; set; }
        public DateTime? DateShipped { get; set; }
        public DateTime? DateDelivered { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}