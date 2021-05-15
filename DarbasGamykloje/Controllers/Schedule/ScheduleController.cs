using System.Linq;
using System.Web;
using System.Web.Mvc;
using DarbasGamykloje.Repos;
using DarbasGamykloje.ViewModels;

namespace DarbasGamykloje.Controllers.Schedule
{
    public class ScheduleController : Controller
    {
        ScheduleRepository ScheduleRepos = new ScheduleRepository();
        AssignmentsRepository AssignmentsRepos = new AssignmentsRepository();
        const int ID = 1;
        // GET: Schedule
        public ActionResult Index()
        {
            ModelState.Clear();
            return View(ScheduleRepos.GetScheduleById(ID));
        }

        public ActionResult FactorySchedule()
        {
            ModelState.Clear();
            return View(ScheduleRepos.GetScheduleByFactoryId(ID));
        }
    }
}