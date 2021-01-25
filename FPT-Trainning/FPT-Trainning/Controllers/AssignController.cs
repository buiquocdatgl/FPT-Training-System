using FPT_Trainning.Models;
using FPT_Trainning.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPT_Trainning.Controllers
{
    public class AssignController : Controller
    {
        private ApplicationDbContext _context;
        public AssignController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: AssignTrainerToCourses
        public ActionResult Index(int id)
        {
            var trainerInCourse = _context.Trainers.Where(c => c.CourseId == id).ToList();
            var traineeInCourse = _context.Trainees.Where(c => c.CourseId == id).ToList();
            var listTrainerAndTraineeInCourse = new ListTrainerAndTraineeInCourse()
            {
                Trainers = trainerInCourse,
                Trainees = traineeInCourse,
                Course = _context.Courses.SingleOrDefault(c => c.Id == id)
            };
            return View(listTrainerAndTraineeInCourse);
        }
        [HttpGet]
        public ActionResult AddTrainerAssign(int id)
        {
            var trainerNotInCourse = _context.Trainers.Where(c => c.CourseId == null).ToList();
            var listTrainerNotInCourse = new ListTrainerAndTraineeInCourse()
            {
                Trainers = trainerNotInCourse,
                Course = _context.Courses.SingleOrDefault(c => c.Id == id)
            };
            return View(listTrainerNotInCourse);
        }

        [HttpPost]
        public ActionResult AddTrainerAssign(string CourseId, string TrainerId)
        {
            int courseId = Convert.ToInt32(CourseId);
            var trainer = _context.Trainers.SingleOrDefault(t => t.TrainerId == TrainerId);
            trainer.CourseId = courseId;
            _context.SaveChanges();
            return RedirectToAction("Index/"+ CourseId);
        }

        [HttpPost]
        public ActionResult DeleteTrainerAssign(string CourseId, string TrainerId)
        {
            int courseId = Convert.ToInt32(CourseId);
            var trainer = _context.Trainers.SingleOrDefault(t => t.TrainerId == TrainerId);
            trainer.CourseId = null;
            _context.SaveChanges();
            return RedirectToAction("Index/" + CourseId);
        }
        [HttpGet]
        public ActionResult AddTraineeAssign(int id)
        {
            var traineeNotInCourse = _context.Trainees.Where(c => c.CourseId == null).ToList();
            var listTraineeNotInCourse = new ListTrainerAndTraineeInCourse()
            {
                Trainees = traineeNotInCourse,
                Course = _context.Courses.SingleOrDefault(c => c.Id == id)
            };
            return View(listTraineeNotInCourse);
        }

        [HttpPost]
        public ActionResult AddTraineeAssign(string CourseId, string TraineeId)
        {
            int courseId = Convert.ToInt32(CourseId);
            var trainee = _context.Trainees.SingleOrDefault(t => t.TraineeId == TraineeId);
            trainee.CourseId = courseId;
            _context.SaveChanges();
            return RedirectToAction("Index/" + CourseId);
        }

        [HttpPost]
        public ActionResult DeleteTraineeAssign(string CourseId, string TraineeId)
        {
            int courseId = Convert.ToInt32(CourseId);
            var trainee = _context.Trainees.SingleOrDefault(t => t.TraineeId == TraineeId);
            trainee.CourseId = null;
            _context.SaveChanges();
            return RedirectToAction("Index/" + CourseId);
        }

        [HttpGet]
        public ActionResult ChangeTrinerAssign(int id)
        {
            var trainerInCourse = _context.Trainers.Where(c => c.CourseId == id).ToList();
            var listTrainerInCourse = new ListTrainerAndTraineeInCourse()
            {
                Trainers = trainerInCourse,
                Course = _context.Courses.SingleOrDefault(c => c.Id == id)
            };
            return View(listTrainerInCourse);
        }


        [HttpPost]
        public ActionResult ChangeTrinerAssign(string CourseId, string TrainerId)
        {
            int courseId = Convert.ToInt32(CourseId);
            var trainer = _context.Trainers.SingleOrDefault(t => t.TrainerId == TrainerId);
            trainer.CourseId = courseId;
            _context.SaveChanges();
            return RedirectToAction("Index/" + CourseId);
        }

    }
}