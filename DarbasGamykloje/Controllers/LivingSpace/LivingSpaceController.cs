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


        public ActionResult Create()
        {
            AddLivingSpaceview LivingSpaceView = new AddLivingSpaceview();
            return View(LivingSpaceView);
        }

        [HttpPost]
        public ActionResult Create(AddLivingSpaceview collection)
        {
            TempData["err"] = "";
            try
            {
                AddNewLivingSpace(collection);
                return RedirectToAction("Index");
            }
            catch (Exception death)
            {
                TempData["err"] = death.Message;
                return RedirectToAction("Index");
            }
        }

        public void AddNewLivingSpace(AddLivingSpaceview collection)
        {
            int numberOfLivingSpaces = collection.roomNumber;
            List<LivingSpaceListView> LivingSpaceList = LivingSpaceRepos.GetLivingSpaces();
            
            foreach (var LS in LivingSpaceList)
            {
                if (collection.adress == LS.adress)
                {
                    TempData["err"] = "Address already exists.";
                }
            }



            if (numberOfLivingSpaces == 1)
            {
                if (collection.adress == null || collection.roomNumber == 0 || collection.maxCapacity == 0)
                {
                    TempData["err"] = "Missing data.";
                    return;
                }
                LivingSpaceRepos.AddNewLivingSpace(collection);
            }
            else
            {
                for (int i = 1; i <= numberOfLivingSpaces; i++)
                {
                    if (collection.adress == null || collection.roomNumber == 0 || collection.maxCapacity == 0)
                    {
                        TempData["err"] = "Missing data.";
                        return;
                    }
                    collection.roomNumber = i;
                    LivingSpaceRepos.AddNewLivingSpace(collection);
                }
            }

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
                    return View(LivingSpaceRepos.GetLivingSpaces());
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