using DarbasGamykloje.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace DarbasGamykloje.ViewModels.WorkSpace
{
    public class AddWorkspaceView
    {
        [DisplayName("Work Name")]
        public string name { get; set; }
        [DisplayName("Work Description")]
        public string description { get; set; }
        [DisplayName("Work ID")]
        public int id_Workspace { get; set; }
        [DisplayName("Factory")]
        [Required]
        public int fk_Factoryid_Factory { get; set; }

        public virtual IList<Assignment> assignments { get; set; }
    }
}