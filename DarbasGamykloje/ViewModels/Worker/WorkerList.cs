using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace DarbasGamykloje.ViewModels
{
    public class WorkerList
    {
        [DisplayName("Name")]
        public string Name { get; set; }
        [DisplayName("Surname")]
        public string Surname { get; set; }
        [DisplayName("Salary")]
        public int Salary { get; set; }
        [DisplayName("Worker ID")]
        public int id_Worker { get; set; }
    }
}