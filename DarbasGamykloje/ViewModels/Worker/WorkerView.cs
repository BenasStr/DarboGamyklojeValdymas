using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace DarbasGamykloje.ViewModels
{
    public class WorkerView
    {
        [DisplayName("Salary")]
        public int salary { get; set; }
        [DisplayName("Is deleted")]
        public bool isDeleted { get; set; }
        [DisplayName("Number of days worked")]
        public int numberOfDaysWorked { get; set; }
        [DisplayName("Checked salary count")]
        public int checkedSalaryCount { get; set; }
        [DisplayName("Worker ID")]
        public int id_Worker { get; set; }
        [DisplayName("Manager ID")]
        public int fk_Managerid_Manager { get; set; }
        [DisplayName("Living space ID")]
        public int fk_LivingSpaceid_LivingSpace { get; set; }
        [DisplayName("User ID")]
        public int fk_RegisteredUserid_RegisteredUser { get; set; }
        [DisplayName("Factory ID")]
        public int fk_Factoryid_Factory { get; set; }

        [DisplayName("Completed work")]
        public int completedWork { get; set; }
    }
}