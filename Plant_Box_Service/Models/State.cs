﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plant_Box_Service.Models
{
    public class State
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}