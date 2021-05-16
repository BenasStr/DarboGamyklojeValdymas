using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using DarbasGamykloje.Repos;
using DarbasGamykloje.ViewModels;
using DarbasGamykloje.ViewModels.WorkSpace;

namespace DarbasGamykloje.Controllers.Schedule
{
    public class ScheduleController : Controller
    {
        ScheduleRepository ScheduleRepos = new ScheduleRepository();
        AssignmentsRepository AssignmentsRepos = new AssignmentsRepository();
        WorkerRepository WorkerRepos = new WorkerRepository();
        WorksSpaceRepository worksSpaceRepos = new WorksSpaceRepository();

        const int ID = 1;
        // GET: Schedule
        public ActionResult Index()
        {
            ModelState.Clear();
            return View(ScheduleRepos.GetScheduleById(ID));
        }

        public ActionResult FactorySchedule(FactoryListView factory)
        {
            List<WorkerView> workers = WorkerRepos.GetFactoryWorkersThatAreFreeBetween(factory.id_Factory,factory.from,factory.to);
            int count = WorkerRepos.GetFactoryWorkerCountBetween(factory.id_Factory, factory.from, factory.to);
            List<AddWorkspaceView> workspaces = worksSpaceRepos.GetFactoryWorkspaces(factory.id_Factory);

            foreach(WorkerView worker in workers)
            {
                ScheduleListView model = new ScheduleListView();
                model.endDate = factory.to;
                model.startDate = factory.from;
                model.fk_Workerid_Worker = worker.id_Worker;
                ScheduleRepos.addSchedule(model);
            }

            return View(ScheduleRepos.GetScheduleByFactoryId(factory.id_Factory));
        }
        public ActionResult ScheduleEditView(int id)
        {
            return View(ScheduleRepos.GetScheduleByItemId(id));
        }

        [HttpPost]
        public ActionResult ScheduleEditView(int id, ScheduleListView collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ScheduleRepos.updateSchedule(collection);
                }

                return RedirectToAction("FactorySchedule");
            }
            catch
            {
                return View(collection);
            }
        }

    }
}