using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DarbasGamykloje.Models
{
    public class Product
    {
        public string Name { get; set; }
        public int IsDeleted { get; set; }
        public int Id_Product { get; set; }
        public int fk_FactoryId { get; set; }
        public double Value { get; set; }
    }
}