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
    [Authorize(Roles = "ADMIN,STAFF")]
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
                    .Where(t => t.ApplicationUser.Email.Contains(searchInput) ||
                    t.ApplicationUser.FullName.Contains(searchInput) ||
                    t.Education.Contains(searchInput))
                    .ToList();
            }
            return View(trainers);
        }

        public ActionResult Details(string id)
        {
            var trainerInDb = _context.Trainers.SingleOrDefault(t => t.TrainerId == id);
            if (trainerInDb == null)
            {
                TempData["MessageError"] = "The Trainer Name doesn't Exist";
                return RedirectToAction("Index");
            }
            return View(trainerInDb);
        }
        [Authorize(Roles = "STAFF")]
        [HttpGet]
        public ActionResult Update(string id)
        {
            var trainerInDb = _context.Trainers.SingleOrDefault(t => t.TrainerId == id);
            if (trainerInDb == null)
            {
                TempData["MessageError"] = "The Trainer Name doesn't Exist";
                return RedirectToAction("Index");
            }
            return View(trainerInDb);
        }
        [Authorize(Roles = "STAFF")]
        [HttpPost]
        public ActionResult Update(Trainer trainer)
        {
            var checkTrainer = _context.Trainers.SingleOrDefault(t => t.TrainerId == trainer.TrainerId);
            if (checkTrainer == null)
            {
                TempData["MessageError"] = "Can not Find Trainer to Update";
                return RedirectToAction("Update");
            }
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
            TempData["MessageSuccess"] = "Update Trainer Successfully";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            if (User.IsInRole("ADMIN"))
            {
                var userInDb = _context.Users.SingleOrDefault(u => u.Id == id);
                if (userInDb == null)
                {
                    TempData["MessageError"] = "The User doesn't Exist";
                    return RedirectToAction("Index");
                }
                var trainerInDb = _context.Trainers.SingleOrDefault(t => t.TrainerId == id);
                if (trainerInDb == null)
                {
                    TempData["MessageError"] = "The Trainer doesn't Exist";
                    return RedirectToAction("Index");
                }
                _context.Trainers.Remove(trainerInDb);
                _context.Users.Remove(userInDb);
                _context.SaveChanges();
                TempData["MessageSuccess"] = "Delete Trainer Successfully";
                return RedirectToAction("Index");
            }
            var trainer = _context.Trainers.SingleOrDefault(t => t.TrainerId == id);
            if (trainer == null)
            {
                TempData["MessageError"] = "The Trainer doesn't Exist";
                return RedirectToAction("Index");
            }
            _context.Trainers.Remove(trainer);
            _context.SaveChanges();
            TempData["MessageSuccess"] = "Delete Trainer Successfully";
            return RedirectToAction("Index");
        }
    }
}