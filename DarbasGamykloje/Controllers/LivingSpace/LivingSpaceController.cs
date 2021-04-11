using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DarbasGamykloje.Repos;
using DarbasGamykloje.ViewModels;
using DarbasGamykloje.Models;

namespace DarbasGamykloje.Controllers.LivingSpace
{
    public class LivingSpaceController : Controller
    {
        LivingSpaceRepository LivingSpaceRepos = new LivingSpaceRepository();

        public ActionResult Index()
        {
            ModelState.Clear();
            return View(LivingSpaceRepos.getLivingSpaces());
        }

        public ActionResult Delete(string adress)
        {
            return View(LivingSpaceRepos.getLivingSpaceByAddress(adress));
        }

        [HttpPost]
        public ActionResult Delete(string adress, FormCollection collection)
        {
            try
            {
                bool used = false;
                if (LivingSpaceRepos.getWorkersInLivingSpaces(adress) > 0)
                {
                    used = true;
                    ViewBag.naudojama = "PEOPLE ARE STILL LOIVING HERE";
                    return View(LivingSpaceRepos.getLivingSpaces());
                }

                if (!used)
                {
                    LivingSpaceRepos.ConfirmDeleteLivingSpace(adress);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}