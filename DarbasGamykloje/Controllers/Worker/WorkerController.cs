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
            WorkerView Worker = WorkerRepos.GetWorkerById(WorkerID);
            return View();
        }
    }
}