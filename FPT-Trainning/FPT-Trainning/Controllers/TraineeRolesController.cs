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
    [Authorize(Roles = "TRAINEE")]
    public class TraineeRolesController : Controller
    {
        private ApplicationDbContext _context;
        public TraineeRolesController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: TraineeRoles
        public ActionResult Profile()
        {
            var userId = User.Identity.GetUserId();
            ApplicationUser currentUser = _context.Users.FirstOrDefault(x => x.Id == userId);
            var traineeInfo = _context.Trainees.SingleOrDefault(t => t.TraineeId == currentUser.Id);

            var userInfoTrainee = new UserInfo()
            {
                user = currentUser,
                trainee = traineeInfo
            };
            return View(userInfoTrainee);
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
            userInDb.PhoneNumber = user.PhoneNumber;
            _context.SaveChanges();
            return RedirectToAction("Profile");
        }
       
        public ActionResult UpdateTraineeProfile()
        {
            var userId = User.Identity.GetUserId();
            ApplicationUser currentUser = _context.Users.FirstOrDefault(x => x.Id == userId);
            var traineeInfo = _context.Trainees.SingleOrDefault(t => t.TraineeId == currentUser.Id);

            var userInfoTrainee = new UserInfo()
            {
                user = currentUser,
                trainee = traineeInfo
            };
            return View(userInfoTrainee);
        }
        [HttpPost]
        public ActionResult UpdateTraineeProfile(Trainee trainee)
        {
            var traineeInfoInDb = _context.Trainees.SingleOrDefault(t => t.TraineeId == trainee.TraineeId);
            traineeInfoInDb.ProgramLanguage = trainee.ProgramLanguage;
            traineeInfoInDb.Age = trainee.Age;
            traineeInfoInDb.DOB = trainee.DOB;
            traineeInfoInDb.Experience = trainee.Experience;
            traineeInfoInDb.Education = trainee.Education;
            traineeInfoInDb.ToeicScore = trainee.ToeicScore;
            _context.SaveChanges();
            return RedirectToAction("Profile");
        }

        public ActionResult ViewCourse()
        {
            var userId = User.Identity.GetUserId();
            ApplicationUser currentUser = _context.Users.SingleOrDefault(x => x.Id == userId);
            var traineeInDb = _context.Trainees.SingleOrDefault(t => t.TraineeId == currentUser.Id);
            if (traineeInDb == null)
            {
                TempData["MessageError"] = "You do not have Trainee Profile";
                return RedirectToAction("Index", "Home");

            }
            var courseTrainee = _context.Courses.SingleOrDefault(c => c.Id == traineeInDb.CourseId);
            if (courseTrainee == null)
            {
                TempData["MessageError"] = "You do not have Course Assign";
                return RedirectToAction("Index", "Home");
            }
            var courses = _context.Courses.Include(c => c.Category).ToList();

            var userInfoTrainee = new UserInfo()
            {
                user = currentUser,
                trainee = traineeInDb
            };
            return View(courseTrainee);
        }
    }
}