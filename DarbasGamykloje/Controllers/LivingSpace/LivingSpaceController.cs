using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DarbasGamykloje.Repos;
using DarbasGamykloje.ViewModels;

namespace DarbasGamykloje.Controllers.LivingSpace
{
    public class LivingSpaceController : Controller
    {
        LivingSpaceRepository LivingSpaceRepos = new LivingSpaceRepository();

        public ActionResult Index()
        {
            ModelState.Clear();
            return View(LivingSpaceRepos.GetLivingSpaces());
        }

        public ActionResult EditLivingSpace(int id)
        {
            EditLivingSpaceView livingspace = LivingSpaceRepos.GetRoomById(id);
            return View(livingspace);
        }

        // POST: Modelis/Edit/5
        [HttpPost]
        public ActionResult EditLivingSpace(int id, EditLivingSpaceView collection)
        {
            try
            {
                // Atnaujina kliento informacija
                if (ModelState.IsValid)
                {
                    LivingSpaceRepos.EditExistingRoom(collection);
                }
                ViewBag.SuccessMessage = "<p>Success!</p>";
                return RedirectToAction("RoomList", new { id = collection.adress });
            }
            catch
            {
                return View(collection);
            }
        }

        public ActionResult RoomList(string id)
        {
            List<EditLivingSpaceView> livingspace = LivingSpaceRepos.GetLivingSpaceById(id);
            return View(livingspace);
        }
    }
}