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
        public double salary { get; set; }
        public bool isDeleted { get; set; }
        public int numberOfDaysWorked { get; set; }
        public int checkSalaryCount { get; set; }
    }
}