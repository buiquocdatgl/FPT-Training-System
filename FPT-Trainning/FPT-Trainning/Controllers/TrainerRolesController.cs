using FPT_Trainning.Models;
using FPT_Trainning.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace FPT_Trainning.Controllers
{
    [Authorize(Roles = "TRAINER")]
    public class TrainerRolesController : Controller
    {
        private ApplicationDbContext _context;
        public TrainerRolesController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: TrainerRoles
        public ActionResult Profile()
        {
            var userId = User.Identity.GetUserId();
            ApplicationUser currentUser = _context.Users.SingleOrDefault(x => x.Id == userId);
            var trainerInfo = _context.Trainers.SingleOrDefault(t => t.TrainerId == currentUser.Id);
            if (trainerInfo == null)
            {
                var userTrainer = new UserInfo()
                {
                    user = currentUser,
                };  
                return View(userTrainer);
            }
            var userInfoTrainer = new UserInfo()
            {
                user = currentUser,
                trainer = trainerInfo
            };
            return View(userInfoTrainer);
        }
        [HttpGet]
        public ActionResult UpdateProfile()
        {
            var userId = User.Identity.GetUserId();
            ApplicationUser user = _context.Users.FirstOrDefault(x => x.Id == userId);
            var userInfo = new UserInfo()
            {
                user = user
            };
            return View(userInfo);
        }
        [HttpPost]
        public ActionResult UpdateProfile(ApplicationUser user)
        {
            var userInDb = _context.Users.SingleOrDefault(u => u.Id == user.Id);
            userInDb.FullName = user.FullName;
            _context.SaveChanges();
            return RedirectToAction("Profile");
        }
        [HttpGet]
        public ActionResult UpdateTrainerProfile()
        {
            var userId = User.Identity.GetUserId();
            ApplicationUser currentUser = _context.Users.FirstOrDefault(x => x.Id == userId);
            var trainerInfo = _context.Trainers.SingleOrDefault(t => t.TrainerId == currentUser.Id);

            var userInfoTrainer = new UserInfo()
            {
                user = currentUser,
                trainer = trainerInfo
            };
            return View(userInfoTrainer);
        }
        [HttpPost]
        public ActionResult UpdateTrainerProfile(Trainer trainer)
        {
            var trainerInfoInDb = _context.Trainers.SingleOrDefault(t => t.TrainerId == trainer.TrainerId);
            trainerInfoInDb.Education = trainer.Education;
            trainerInfoInDb.Phone = trainer.Phone;
            trainerInfoInDb.WorkingPlace = trainer.WorkingPlace;
            trainerInfoInDb.Type = trainer.Type;
            _context.SaveChanges();
            return RedirectToAction("Profile");
        }
        public ActionResult ViewCourse()
        {
            var userId = User.Identity.GetUserId();
            ApplicationUser currentUser = _context.Users.SingleOrDefault(x => x.Id == userId);

            var trainerInDb = _context.Trainers.SingleOrDefault(t => t.TrainerId == currentUser.Id);
            if (trainerInDb == null)
            {
                TempData["MessageError"] = "You do not have Trainer Profile";
                return RedirectToAction("Index", "Home");

            }
            var courseTrainer = _context.Courses.Include(c => c.Category).SingleOrDefault(c => c.Id == trainerInDb.CourseId);
            if (courseTrainer == null)
            {
                TempData["MessageError"] = "You do not have Course Assign";
                return RedirectToAction("Index", "Home");
            }
            return View(courseTrainer);
        }
    }
}