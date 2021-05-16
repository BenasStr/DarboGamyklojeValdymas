using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DarbasGamykloje.Repos;
using DarbasGamykloje.ViewModels.WorkSpace;
using DarbasGamykloje.ViewModels;
using DarbasGamykloje.ViewModels.Factory;

namespace DarbasGamykloje.Controllers.FactoryManagment
{
    public class FactoryController : Controller
    {
        FactoryRepository factoryrepos = new FactoryRepository();
        WorkerRepository workerRepository = new WorkerRepository();
        AssignmentsRepository assignmentsRepository = new AssignmentsRepository();
        ScheduleRepository scheduleRepository = new ScheduleRepository();

        public ActionResult Index()
        {
            return View(factoryrepos.GetAllFactories());
        }
        
        public ActionResult FactoryListForSelectingWorkers()
        {
            return View(factoryrepos.GetAllFactories());
        }

        public ActionResult WorkerSalary(int id, string name, string surname)
        {
            WorkerList worker = new WorkerList();
            worker.Name = name;
            worker.Surname = surname;
            worker.id_Worker = id;

            string type = workerRepository.CheckWorkerType(id);

            if (type == "MotivatedWorker")
            {
                int count = assignmentsRepository.CountsCompletedAssignments(id);
                worker.Salary = count * 10;
            } else
            {
                int count = scheduleRepository.CountDaysWorked(id);
                worker.Salary = count * 8;
            }


            return View(worker);
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

        public ActionResult WorkerListInFactory(int id)
        {
            return View(workerRepository.GetWorkerByFactoryId(id));
        }
    }
}