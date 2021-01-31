using FPT_Trainning.Models;
using FPT_Trainning.ViewModel;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace FPT_Trainning.Controllers
{
    public class TraineesController : Controller
    {
        private ApplicationDbContext _context;
        public TraineesController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Trainers
        public ActionResult Index(string searchInput)
        {
            var trainees = _context.Trainees.ToList();
            if (!searchInput.IsNullOrWhiteSpace())
            {
                trainees = _context.Trainees
                    .Where(t => t.ApplicationUser.FullName.Contains(searchInput))
                    .ToList();
            }
            return View(trainees);
        }

        public ActionResult Details(string id)
        {
            var traineeInDb = _context.Trainees.SingleOrDefault(t => t.TraineeId == id);
            return View(traineeInDb);
        }

        [HttpGet]
        public ActionResult Update(string id)
        {
            var traineeInDb = _context.Trainees.SingleOrDefault(t => t.TraineeId == id);
            if (traineeInDb == null)
            {
                return HttpNotFound();
            }
            return View(traineeInDb);
        }

        [HttpPost]
        public ActionResult Update(Trainee trainee)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var traineeInDb = _context.Trainees.SingleOrDefault(t => t.TraineeId == trainee.TraineeId);
            {
                traineeInDb.ApplicationUser.FullName = trainee.ApplicationUser.FullName;
                traineeInDb.ProgramLanguage = trainee.ProgramLanguage;
                traineeInDb.Age = trainee.Age;
                traineeInDb.DOB = trainee.DOB;
                traineeInDb.Experience = trainee.Experience;
                traineeInDb.Education = trainee.Education;
                traineeInDb.Location = trainee.Location;
                traineeInDb.ToeicScore = trainee.ToeicScore;
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            var traineeInDb = _context.Trainees.SingleOrDefault(t => t.TraineeId == id);

            if (traineeInDb == null) return HttpNotFound();

            _context.Trainees.Remove(traineeInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}