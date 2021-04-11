using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DarbasGamykloje.ViewModels
{
    public class AddLivingSpaceview
    {
        
        [DisplayName("Adress")]
        [Required]
        public string adress { get; set; }

        [DisplayName("Room Number")]
        public int roomNumber { get; set; }

        [DisplayName("Max Capacity")]
        public int maxCapacity { get; set; }

    }
}