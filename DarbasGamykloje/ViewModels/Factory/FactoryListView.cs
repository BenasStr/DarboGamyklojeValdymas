﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace DarbasGamykloje.ViewModels
{
    public class FactoryListView
    {
        [DisplayName("Max Capacity")]
        public int maxCapacity { get; set; }
        [DisplayName("Factory ID")]
        public int id_Factory { get; set; }
        public int fk_Managerid_Manager { get; set; }

        public DateTime from { get; set; }

        public DateTime to { get; set; }

    }
}