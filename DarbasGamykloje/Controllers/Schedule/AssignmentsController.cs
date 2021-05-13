using System.Linq;
using System.Web;
using System.Web.Mvc;
using DarbasGamykloje.Repos;
using DarbasGamykloje.ViewModels;

namespace DarbasGamykloje.Controllers.Assignments
{
    public class AssignmentsController : Controller
    {
        const int ID = 1;
        AssignmentsRepository AssignmentsRepos = new AssignmentsRepository();
        // GET: Aassignments
        public ActionResult Index()
        {
            ModelState.Clear();
            return View(AssignmentsRepos.GetAssignments(ID));
        }

        public ActionResult AssignmentView(int id)
        {
            return View(AssignmentsRepos.GetDetailedAssignmentById(id));
        }
    }
}