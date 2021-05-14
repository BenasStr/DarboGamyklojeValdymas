using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Generator;
using DarbasGamykloje.Models;
using DarbasGamykloje.Repos;
using DarbasGamykloje.ViewModels.WorkSpace;

namespace DarbasGamykloje.Controllers.FactoryManagment
{
    public class FactoryController : Controller
    {
        FactoryRepository factoryrepos = new FactoryRepository();
        WorksSpaceRepository workrepos = new WorksSpaceRepository();

        private void populateList(AddWorkspaceView model)
        {
            if (model.assignments == null)
            {
                model.assignments = new List<Assignment>();
                model.assignments.Add(new Assignment());
                model.assignments[0].startTime = null;
                model.assignments[0].endTime = null;
            }
        }
        // GET: Factory
        public ActionResult Index()
        {
            return View(factoryrepos.GetAllFactories());
        }

        public ActionResult Add(int id)
        {
            AddWorkspaceView workspace = new AddWorkspaceView();
            workspace.fk_Factoryid_Factory = id;
            populateList(workspace);
            return View(workspace);
        }

        [HttpPost]
        public ActionResult Add(AddWorkspaceView model)
        {
            int a = workrepos.GetLatestWorkspaceId();
            return View(model);
            List<AddWorkspaceView> workspaced = workrepos.GetAllWorkSpaces(model.fk_Factoryid_Factory);
            foreach (var i in workspaced) 
            {
               if (i.name.Equals(model.name))
                {
                    ViewBag.error = "Name already exists";
                    return View(model);
                }
            }
            if (workspaced.Count() == factoryrepos.GetFactoryCapacity(model.fk_Factoryid_Factory))
            {
                ViewBag.error = "Factory reached Max Capacity";
                return View(model);
            }
            if (model.assignments.Count <= 0)
            {
                ViewBag.error = "Need Atleast 1 assignment";
                return View(model);
            }
            factoryrepos.addWorkspace(model);
            return RedirectToAction("Index");
        }


    }
}