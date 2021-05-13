using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DarbasGamykloje.Repos;
using DarbasGamykloje.ViewModels.WorkSpace;


namespace DarbasGamykloje.Controllers.FactoryManagment
{
    public class WorkSpaceController : Controller
    {
        FactoryRepository factoryrepos = new FactoryRepository();
        WorksSpaceRepository workspacerepos = new WorksSpaceRepository();
        //public bool AddNewWorkSpace(AddWorkspaceView WorkSpaceView)
        //{

        //}


        public void PopulateSelections(AddWorkspaceView WorkSpaceView)
        {
            var factories = factoryrepos.GetAllFactories();
            List<SelectListItem> selectListFactories = new List<SelectListItem>();
            List<SelectListGroup> groups = new List<SelectListGroup>();
            bool yra = false;

            foreach (var item in factories)
            {
                selectListFactories.Add(new SelectListItem() { Value = Convert.ToString(item.id_Factory) });
            }
        }


    }
}