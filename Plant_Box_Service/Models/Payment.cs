using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Plant_Box_Service.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public double AmountDue { get; set; }
        public double TotalPaid { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}