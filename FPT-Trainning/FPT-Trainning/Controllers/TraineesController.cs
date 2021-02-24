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
    [Authorize(Roles = "STAFF")]
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
                    .Where(t => t.ApplicationUser.Email.Contains(searchInput) ||
                    t.ApplicationUser.FullName.Contains(searchInput) ||
                    t.ProgramLanguage.Contains(searchInput) || 
                    t.Education.Contains(searchInput) ||
                    t.Location.Contains(searchInput))
                    .ToList();
            }
            return View(trainees);
        }

        public ActionResult Details(string id)
        {
            var traineeInDb = _context.Trainees.SingleOrDefault(t => t.TraineeId == id);
            if (traineeInDb == null)
            {
                TempData["MessageError"] = "The Trainee Name doesn't Exist";
                return RedirectToAction("Index");
            }
            return View(traineeInDb);
        }

        [HttpGet]
        public ActionResult Update(string id)
        {
            var traineeInDb = _context.Trainees.SingleOrDefault(t => t.TraineeId == id);
            if (traineeInDb == null)
            {
                TempData["MessageError"] = "The Trainee Name doesn't Exist";
                return RedirectToAction("Index");
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
            var traineeInDb =_context.Trainees.SingleOrDefault(t => t.TraineeId == trainee.TraineeId);
            
            if (traineeInDb == null)
            {
                TempData["MessageError"] = "Can not Find Trainee to Update";
                return RedirectToAction("Update");
            }
            traineeInDb.ApplicationUser.FullName = trainee.ApplicationUser.FullName;
            traineeInDb.ProgramLanguage = trainee.ProgramLanguage;
            traineeInDb.Age = trainee.Age;
            traineeInDb.DOB = trainee.DOB;
            traineeInDb.Experience = trainee.Experience;
            traineeInDb.Education = trainee.Education;
            traineeInDb.Location = trainee.Location;
        
            _context.SaveChanges();
            TempData["MessageSuccess"] = "Update Trainee Successfully";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
                var userInDb = _context.Users.SingleOrDefault(u => u.Id == id);
                if (userInDb == null)
                {
                    TempData["MessageError"] = "The User doesn't Exist";
                    return RedirectToAction("Index");
                }
                var traineeInDb = _context.Trainees.SingleOrDefault(t => t.TraineeId == id);
                if (traineeInDb == null)
                {
                    TempData["MessageError"] = "The Trainee doesn't Exist";
                    return RedirectToAction("Index");
                }
                _context.Trainees.Remove(traineeInDb);
                _context.Users.Remove(userInDb);
                _context.SaveChanges();
                TempData["MessageSuccess"] = "Delete Trainee Successfully";
                return RedirectToAction("Index");
        }
    }
}