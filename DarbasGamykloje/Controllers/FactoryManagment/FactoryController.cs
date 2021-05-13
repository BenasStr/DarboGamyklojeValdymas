using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DarbasGamykloje.Repos;
using DarbasGamykloje.ViewModels.WorkSpace;

namespace DarbasGamykloje.Controllers.FactoryManagment
{
    public class FactoryController : Controller
    {
        FactoryRepository factoryrepos = new FactoryRepository();
        // GET: Factory
        public ActionResult Index()
        {
            return View(factoryrepos.GetAllFactories());
        }

        public ActionResult Add(int id)
        {
            AddWorkspaceView workspace = new AddWorkspaceView();
            workspace.fk_Factoryid_Factory = id;
            return View(workspace);
        }

        [HttpPost]
        public ActionResult Add(AddWorkspaceView model)
        {
            factoryrepos.addWorkspace(model);
            return RedirectToAction("Index");
        }


    }
}