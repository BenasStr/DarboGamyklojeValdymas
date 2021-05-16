using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DarbasGamykloje.ViewModels.Factory
{
    public class ProfitEvaluationView
    {
        [DisplayName("Profit")]
        public double Profit { get; set; }

        [DisplayName("Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime date { get; set; }

        [DisplayName("Factory")]
        public int fk_factoryId { get; set; }

        public IList<SelectListItem> FactoryList { get; set; }
    }
}