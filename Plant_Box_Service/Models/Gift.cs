using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Plant_Box_Service.Models
{
    public class Gift
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        [ForeignKey("State")]
        public int? StateId{ get; set; }
        public State State { get; set; }
        public int? ZipCode { get; set; }
        public bool? isContinuous { get; set; }
        public bool? Status { get; set; }
    }
}