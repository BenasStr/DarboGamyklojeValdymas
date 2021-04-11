using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DarbasGamykloje.ViewModels
{
    public class EditLivingSpaceView
    {
        [DisplayName("Adress")]
        public string adress { get; set; }

        [DisplayName("Room Number")]
        public int roomNumber { get; set; }

        [DisplayName("Max Capacity")]
        [Required]
        public int maxCapacity { get; set; }

        [DisplayName("ID")]
        public int id_LivingSpace { get; set; }
    }
}