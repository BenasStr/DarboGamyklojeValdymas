using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace DarbasGamykloje.ViewModels
{
    public class ScheduleListView
    {
        [DisplayName("Start date")]
        public DateTime startDate { get; set; }
        [DisplayName("End date")]
        public DateTime endDate { get; set; }
        [DisplayName("Worker ID")]
        public int fk_Workerid_Worker { get; set; }
        [DisplayName("ID")]
        public int id_Schedule { get; set; }
    }
}