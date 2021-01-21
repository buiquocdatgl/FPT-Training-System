using FPT_Trainning.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPT_Trainning.Controllers
{
	public class CoursesController : Controller
	{
		private ApplicationDbContext _context;
		public CoursesController()
		{
			_context = new ApplicationDbContext();
		}

		public ActionResult Index(string searchString)
		{
			var courses = _context.Courses.ToList();

			if (!searchString.IsNullOrWhiteSpace())
			{
				courses = _context.Courses
					.Where(c => c.Name.Contains(searchString))
					.ToList();
			}

			return View(courses);
		}

		public ActionResult Details(int id)
		{
			Course courseInDb = _context.Courses.SingleOrDefault(c => c.Id == id);
			return View(courseInDb);
		}

		public ActionResult Delete(int id)
		{
			var courseInDb = _context.Courses.SingleOrDefault(c => c.Id == id);

			if (courseInDb == null) return HttpNotFound();

			_context.Courses.Remove(courseInDb);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		[HttpGet]
		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(Course course)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}
			var newCourse = new Course()
			{
				Name = course.Name
			};

			_context.Courses.Add(newCourse);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		[HttpGet]
		public ActionResult Edit(int id)
		{
			var coursesInDb = _context.Courses.SingleOrDefault(c => c.Id == id);
			if (coursesInDb == null) return HttpNotFound();

			return View();
		}

		[HttpPost]
		public ActionResult Edit(Course course)

		{
			if (!ModelState.IsValid)
			{
				return View();
			}
			var courseInDb = _context.Courses.SingleOrDefault(c => c.Id == course.Id);

			courseInDb.Name = course.Name;
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}