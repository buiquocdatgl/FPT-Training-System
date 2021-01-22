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

	public class CoursesController : Controller
	{
        private ApplicationDbContext _context;
        public CoursesController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var courses = _context.Courses
                .Include(m => m.Category)
                .ToList();
            return View(courses);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = new CourseCategoriesViewModel()
            {
                Categories = _context.Categories.ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Course course)
        {
            var newCourse = new Course()
            {
                Name = course.Name,
                Description = course.Description,
                CategoryId = course.CategoryId

            };
            _context.Courses.Add(newCourse);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var coursesInDb = _context.Courses.SingleOrDefault(t => t.Id == id);
            return View(coursesInDb);
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var courseInDb = _context.Courses.SingleOrDefault(t => t.Id == id);
            if (courseInDb == null)
            {
                return HttpNotFound();
            }
            var viewModel = new CourseCategoriesViewModel()
            {
                Course = courseInDb,
                Categories = _context.Categories.ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Update(Course course)
        {
            var courseInDb = _context.Courses.SingleOrDefault(t => t.Id == course.Id);

            courseInDb.Name = course.Name;
            courseInDb.Description = course.Description;
            courseInDb.CategoryId = course.CategoryId;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var courseInDb = _context.Courses.SingleOrDefault(c => c.Id == id);

            if (courseInDb == null) return HttpNotFound();

            _context.Courses.Remove(courseInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}