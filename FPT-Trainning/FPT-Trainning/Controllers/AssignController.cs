using FPT_Trainning.Models;
using FPT_Trainning.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPT_Trainning.Controllers
{
    [Authorize(Roles = "STAFF")]
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
            var checkCourse = _context.Courses.SingleOrDefault(c => c.Id == id);
            if (checkCourse == null)
            {
                TempData["MessageError"] = "Can not Find Course For Management Assign";
                return RedirectToAction("Index", "Courses");
            }
            var listTrainerAndTraineeInCourse = new ListTrainerAndTraineeInCourse()
            {
                Trainers = trainerInCourse,
                Trainees = traineeInCourse,
                Course = checkCourse
            };
            return View(listTrainerAndTraineeInCourse);
        }
        [HttpGet]
        public ActionResult AddTrainerAssign(int id)
        {
            var checkCourse = _context.Courses.SingleOrDefault(c => c.Id == id);
            if (checkCourse == null)
            {
                TempData["MessageError"] = "Can not Find Course For Management Assign";
                return RedirectToAction("Index", "Courses");
            }
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
            if (trainer == null)
            {
                TempData["MessageError"] = "Can not Find Trainer For Add Assign";
                return RedirectToAction("Index");
            }
            trainer.CourseId = courseId;
            _context.SaveChanges();
            TempData["MessageSuccess"] = "Add Trainer Assign Successfully";
            return RedirectToAction("Index", new { id = courseId});
        }

        [HttpPost]
        public ActionResult DeleteTrainerAssign(string CourseId, string TrainerId)
        {
            int courseId = Convert.ToInt32(CourseId);
            var trainer = _context.Trainers.SingleOrDefault(t => t.TrainerId == TrainerId);
            if (trainer == null)
            {
                TempData["MessageError"] = "Can not Find Trainer For Delete Assign";
                return RedirectToAction("Index", new { id = courseId });
            }
            trainer.CourseId = null;
            _context.SaveChanges();
            TempData["MessageSuccess"] = "Delete Trainer Assign Successfully";
            return RedirectToAction("Index", new { id = courseId });
        }
        [HttpGet]
        public ActionResult ChangeTrainerAssign(int CourseId, string TrainerId)
        {
            var CoursesExclude = _context.Courses.Where(c => c.Id != CourseId);
            if (CoursesExclude.Count() == 0)
            {
                TempData["MessageError"] = "Can't find Course to Change";
                return RedirectToAction("Index" ,new { id = CourseId});
            }
            var checkTrainer = _context.Trainers.SingleOrDefault(t => t.TrainerId == TrainerId);
            if (checkTrainer == null)
            {
                TempData["MessageError"] = "Can not Find Trainer For Change Assign";
                return RedirectToAction("Index");
            }
            else
            {
                var checkCourse = _context.Courses.SingleOrDefault(c => c.Id == CourseId);
                if (checkCourse == null)
                {
                    TempData["MessageError"] = "Can not Find Course For Change Assign";
                    return RedirectToAction("Index", "Courses");
                }
                else
                {
                    if (checkTrainer.CourseId != checkCourse.Id)
                    {

                        TempData["MessageError"] = "Can not Find Course For Change Assign";
                        return RedirectToAction("Index");

                    }
                }
            }

            var changeViewmodel = new ChangeTrainerAssign()
            {
                Courses = CoursesExclude,
                Trainer = checkTrainer,
            };
            return View(changeViewmodel);

        }


        [HttpPost]
        public ActionResult ChangeTrainerAssign(string CourseId, string TrainerId)
        {
            int courseId = Convert.ToInt32(CourseId);
            var trainer = _context.Trainers.SingleOrDefault(t => t.TrainerId == TrainerId);
            trainer.CourseId = courseId;
            _context.SaveChanges();
            TempData["MessageSuccess"] = "Chane Trainer Assign Successfully";
            return RedirectToAction("Index", new { id = courseId });
        }

        [HttpGet]
        public ActionResult AddTraineeAssign(int id)
        {
            var checkCourse = _context.Courses.SingleOrDefault(c => c.Id == id);
            if (checkCourse == null)
            {
                TempData["MessageError"] = "Can not Find Course For Management Assign";
                return RedirectToAction("Index", "Courses");
            }
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
            if (trainee == null)
            {
                TempData["MessageError"] = "Can not Find Trainee For Add Assign";
                return RedirectToAction("Index");
            }
            trainee.CourseId = courseId;
            _context.SaveChanges();
            TempData["MessageSuccess"] = "Add Trainee Assign Successfully";
            return RedirectToAction("Index", new { id = courseId });
        }

        [HttpPost]
        public ActionResult DeleteTraineeAssign(string CourseId, string TraineeId)
        {
            int courseId = Convert.ToInt32(CourseId);
            var trainee = _context.Trainees.SingleOrDefault(t => t.TraineeId == TraineeId);
            if (trainee == null)
            {
                TempData["MessageError"] = "Can not Find Trainer For Delete Assign";
                return RedirectToAction("Index", new { id = courseId });
            }
            trainee.CourseId = null;
            _context.SaveChanges();
            TempData["MessageSuccess"] = "Delete Trainee Assign Successfully";
            return RedirectToAction("Index", new { id = courseId });
        }

        [HttpGet]
        public ActionResult ChangeTraineeAssign(int CourseId, string TraineeId)
        {
            var checkTrainee = _context.Trainees.SingleOrDefault(t => t.TraineeId == TraineeId);
            if (checkTrainee == null)
            {
                TempData["MessageError"] = "Can not Find Trainee For Change Assign";
                return RedirectToAction("Index");
            }
            else
            {
                var checkCourse = _context.Courses.SingleOrDefault(c => c.Id == CourseId);
                if (checkCourse == null)
                {
                    TempData["MessageError"] = "Can not Find Course For Change Assign";
                    return RedirectToAction("Index", "Courses");
                }
                else
                {
                    if (checkTrainee.CourseId != checkCourse.Id)
                    {

                        TempData["MessageError"] = "Can not Find Course For Change Assign";
                        return RedirectToAction("Index");

                    }
                }
            }
            var CoursesExclude = _context.Courses.Where(c => c.Id != CourseId);

            var changeViewmodel = new ChangeTraineeAssign()
            {
                Courses = CoursesExclude,
                Trainee = checkTrainee,
            };
            return View(changeViewmodel);
        }


        [HttpPost]
        public ActionResult ChangeTraineeAssign(string CourseId, string TraineeId)
        {
            int courseId = Convert.ToInt32(CourseId);
            var trainee = _context.Trainees.SingleOrDefault(t => t.TraineeId == TraineeId);
            trainee.CourseId = courseId;
            _context.SaveChanges();
            TempData["MessageSuccess"] = "Chane Trainer Assign Successfully";
            return RedirectToAction("Index", new { id = courseId });
        }

    }
}