using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DarbasGamykloje.ViewModels
{
    public class DeleteLivingSpaceView
    {
        [DisplayName("Address")]
        public string address { get; set; }
    }
}