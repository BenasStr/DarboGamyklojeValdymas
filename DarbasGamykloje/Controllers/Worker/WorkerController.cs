using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DarbasGamykloje.Repos;
using DarbasGamykloje.ViewModels;

namespace DarbasGamykloje.Controllers.Worker
{
    public class WorkerController : Controller
    {
        WorkerRepository WorkerRepos = new WorkerRepository();
        AssignmentsRepository AssignmentsRepos = new AssignmentsRepository();
        ScheduleRepository ScheduleRepos = new ScheduleRepository();
        const int WorkerID = 1;
        // GET: Worker
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CompletedWork()
        {
            int completedAssignments = 0;
            WorkerView Worker = WorkerRepos.GetWorkerById(WorkerID);
            List<ScheduleListView> Schedules = ScheduleRepos.GetScheduleById(Worker.id_Worker);
            foreach(var schedule in Schedules)
            {
                completedAssignments += AssignmentsRepos.GetCompletedAssignmentCount(schedule.id_Schedule);
            }
            Worker.completedWork = completedAssignments;
            return View(Worker);
        }
    }
}