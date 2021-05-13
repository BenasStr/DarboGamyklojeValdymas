using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DarbasGamykloje.Models
{
    public class WorksSpace
    {
        public string name { get; set; }
        public string description { get; set; }

        public int id_Workspace { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int fk_Factoryid_Factory { get; set; }
    }
}