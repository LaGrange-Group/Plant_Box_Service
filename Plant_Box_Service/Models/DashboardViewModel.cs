﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plant_Box_Service.Models
{
    public class DashboardViewModel
    {
        public bool UpdatePreferences { get; set; }
        public bool UpdatePersonal { get; set; }
        public Customer Customer { get; set; }
        public Gift Gift { get; set; }
        public Preference Preference { get; set; }
        public Payment Payment { get; set; }
        public IEnumerable<Payment> Payments { get; set; }
        public IEnumerable<Shipment> Shipments { get; set; }
        public IEnumerable<State> States { get; set; }
    }
}