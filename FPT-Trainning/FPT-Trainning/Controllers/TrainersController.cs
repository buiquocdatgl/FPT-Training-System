using FPT_Trainning.Models;
using FPT_Trainning.ViewModel;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace FPT_Trainning.Controllers
{
    public class TrainersController : Controller
    {
        private ApplicationDbContext _context;
        public TrainersController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Trainers
        public ActionResult Index(string searchInput)
        {
            var trainers = _context.Trainers.ToList();
            if (!searchInput.IsNullOrWhiteSpace())
            {
                trainers = _context.Trainers
                    .Where(t => t.ApplicationUser.FullName.Contains(searchInput))
                    .ToList();
            }
            return View(trainers);
        }

        public ActionResult Details(string id)
        {
            var trainerInDb = _context.Trainers.SingleOrDefault(t => t.TrainerId == id);
            return View(trainerInDb);
        }

        [HttpGet]
        public ActionResult Update(string id)
        {
            var trainerInDb = _context.Trainers.SingleOrDefault(t => t.TrainerId == id);
            if (trainerInDb == null)
            {
                return HttpNotFound();
            }
            return View(trainerInDb);
        }

        [HttpPost]
        public ActionResult Update(Trainer trainer)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
           var trainerInDb = _context.Trainers.SingleOrDefault(t => t.TrainerId == trainer.TrainerId);
            {
                trainerInDb.ApplicationUser.FullName = trainer.ApplicationUser.FullName;
                trainerInDb.Education = trainer.Education;
                trainerInDb.WorkingPlace = trainer.WorkingPlace;
                trainerInDb.Type = trainer.Type;
                trainerInDb.Phone = trainer.Phone;
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            var trainerInDb = _context.Trainers.SingleOrDefault(t => t.TrainerId == id);

            if (trainerInDb == null) return HttpNotFound();

            _context.Trainers.Remove(trainerInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}