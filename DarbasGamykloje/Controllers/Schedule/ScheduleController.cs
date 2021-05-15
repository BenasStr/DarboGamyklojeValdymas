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

        public ActionResult FactorySchedule(int id, DateTime from, DateTime to)
        {
            List<WorkerView> workers = WorkerRepos.GetFactoryWorkersThatAreFreeBetween(id,from,to);
            List<AddWorkspaceView> workspaces = worksSpaceRepos.GetFactoryWorkspaces(id);

            foreach(WorkerView worker in workers)
            {
                ScheduleListView model = new ScheduleListView();
                model.endDate = to;
                model.startDate = from;
                model.fk_Workerid_Worker = worker.id_Worker;
                ScheduleRepos.addSchedule(model);
            }

            return View(ScheduleRepos.GetScheduleByFactoryId(id));
        }
    }
}