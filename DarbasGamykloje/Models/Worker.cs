using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DarbasGamykloje.Models
{
    public class Worker
    {
        public int salary { get; set; }
        public bool isDeleted { get; set; }
        public int numberOfDaysWorked { get; set; }
        public int checkedSalaryCount { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_Worker { get; set; }
        public int fk_Managerid_Manager { get; set; }
        public int fk_LivingSpaceid_LivingSpace { get; set; }
        public int fk_RegisteredUserid_RegisteredUser { get; set; }
        public int fk_Factoryid_Factory { get; set; }
    }
}