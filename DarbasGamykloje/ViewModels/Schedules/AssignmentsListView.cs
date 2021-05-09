using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace DarbasGamykloje.ViewModels
{
    public class AssignmentsListView
    {
        [DisplayName("Start time")]
        public DateTime startTime { get; set; }
        [DisplayName("End time")]
        public DateTime endTime { get; set; }
        [DisplayName("Is completed")]
        public bool isCompleted { get; set; }
        [DisplayName("Workspace ID")]
        public int fk_Workspaceid_Workspace { get; set; }
        [DisplayName("Schedule ID")]
        public int fk_Scheduleid_Schedule { get; set; }
        [DisplayName("ID")]
        public int id_Assignments { get; set; }
    }
}