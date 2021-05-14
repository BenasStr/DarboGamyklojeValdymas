using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DarbasGamykloje.Models
{
    public class Assignments
    {
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public bool isCompleted { get; set; }
        public int fk_Workspaceid_Workspace { get; set; }
        public int fk_Scheduleid_Schedule { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_Assignments { get; set; }
    }
}