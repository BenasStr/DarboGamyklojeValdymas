using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DarbasGamykloje.Models
{
    public class Factory
    {
        public int maxCapacity { get; set; }
        public int id_Factory { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int fk_Managerid_Manager { get; set; }

    }
}