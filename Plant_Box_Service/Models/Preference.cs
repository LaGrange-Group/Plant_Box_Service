using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plant_Box_Service.Models
{
    public class Preference
    {
        [Key]
        public int Id { get; set; }
        public bool IsLarge { get; set; }
        public string OptimalSize { get; set; }
        public bool FlexibleSize { get; set; }
        public bool DrapeClimb { get; set; }
        public bool Cacti { get; set; }
        public bool Hanging { get; set; }
        public bool FoodBearing { get; set; }
        public bool Bonsai { get; set; }
        public bool Tillandsia { get; set; }
        public bool Flowers { get; set; }
        public bool? PerishablesOnly { get; set; }
    }
}