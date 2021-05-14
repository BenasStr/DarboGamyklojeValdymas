using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace DarbasGamykloje.ViewModels
{
    public class AssignmentDetailsView
    {
        [DisplayName("Name")]
        public string name { get; set; }

        [DisplayName("Description")]
        public string description { get; set; }
    }
}