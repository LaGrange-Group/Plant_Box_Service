using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plant_Box_Service.Models
{
    public class RegistrationViewModel
    {
        public Customer Customer { get; set; }
        public Preference Preference { get; set; }
        public IEnumerable<State> States { get; set; }
    }


}