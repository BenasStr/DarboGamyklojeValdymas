using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace DarbasGamykloje.ViewModels
{
    public class LivingSpaceListView
    {
        [DisplayName("Adress")]
        public string adress { get; set; }

        [DisplayName("Room Number")]
        public int roomNumber { get; set; }

        [DisplayName("Max Capacity")]
        public int maxCapacity { get; set; }

        [DisplayName("ID")]
        public int id_LivingSpace { get; set; }
    }
}