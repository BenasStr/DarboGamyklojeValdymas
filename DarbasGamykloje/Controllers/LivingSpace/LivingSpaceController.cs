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
        WorkerRepository WorkerRepo = new WorkerRepository();
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

        public ActionResult RoomList(string id)
        {
            List<EditLivingSpaceView> livingspace = LivingSpaceRepos.GetLivingSpaceById(id);
            return View(livingspace);
        }

        public ActionResult Delete(string id)
        {
            DeleteLivingSpaceView adresas = new DeleteLivingSpaceView();
            adresas.address = id;

            return View(adresas);
        }

        [HttpPost]
        public ActionResult Delete(DeleteLivingSpaceView collection)
        {
            List<WorkerView> workers = new List<WorkerView>();
            workers = WorkerRepo.GetAllWorkers();

            List<LivingSpaceListView> livingspaces = new List<LivingSpaceListView>();
            livingspaces = LivingSpaceRepos.getLivingSpaceByAddress(collection.address);

            bool rado = false;
            foreach(var a in livingspaces)
            {
                foreach(var b in workers)
                {
                    if(a.id_LivingSpace == b.fk_LivingSpaceid_LivingSpace)
                    {
                        rado = true;
                        break;
                    }
                }
            }

            if(rado == false)
            {
                LivingSpaceRepos.ConfirmDeleteLivingSpace(collection.address);
                return RedirectToAction("Index");
            }

            TempData["err"] = "There are still users registered to this address";
            return View();
        }

        public ActionResult EditLivingSpace(int id)
        {
            EditLivingSpaceView livingspace = LivingSpaceRepos.GetRoomById(id);
            return View(livingspace);
        }

        [HttpPost]
        public ActionResult EditLivingSpace(int id, EditLivingSpaceView collection)
        {
            TempData["err"] = "";
            try
            {
                // Atnaujina kliento informacija
                if (ModelState.IsValid)
                {
                    LivingSpaceRepos.EditExistingRoom(collection);
                }

                return RedirectToAction("RoomList", new { id = collection.adress });
            }
            catch
            {
                return View(collection);
            }
        }
    }
}