﻿using FPT_Trainning.Models;
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
                .Include(c => c.Category)
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
            if (course.Name == "")
            {
                TempData["MessageError"] = "Please input the Course Name";
                return RedirectToAction("Create");
            }
            var checkCourse = _context.Courses.Where(t => t.Name == course.Name);
            if (checkCourse.Count() > 0)
            {
                TempData["MessageError"] = "The Course Name already Existed";
                return RedirectToAction("Create");
            }
            if (!ModelState.IsValid)
            {
                TempData["MessageError"] = "Can Not Create Course";
                return RedirectToAction("Create");
            }
            else
            {
                var newCourse = new Course()
                {
                    Name = course.Name,
                    Description = course.Description,
                    CategoryId = course.CategoryId

                };
                _context.Courses.Add(newCourse);
                _context.SaveChanges();
            }
            TempData["MessageSuccess"] = "Create Course Successfully";
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var coursesInDb = _context.Courses.SingleOrDefault(t => t.Id == id);
            if (coursesInDb == null)
            {
                TempData["MessageError"] = "The Course Name doesn't Exist";
                return RedirectToAction("Index");
            }
            var courses = _context.Courses.Include(c => c.Category).ToList();
            return View(coursesInDb);
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var courseInDb = _context.Courses.SingleOrDefault(t => t.Id == id);
            if (courseInDb == null)
            {
                TempData["MessageError"] = "The Course Name doesn't Exist";
                return RedirectToAction("Index");
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
            if (course.Name == "")
            {
                TempData["MessageError"] = "Please input the Course Name";
                return RedirectToAction("Update");
            }
            var courseInDb = _context.Courses.SingleOrDefault(t => t.Id == course.Id);
            if (courseInDb == null)
            {
                TempData["MessageError"] = "The Course doesn't Exist";
                return RedirectToAction("Index");
            }
            var checkCourse = _context.Courses.Where(t => t.Name == course.Name);
            if (checkCourse.Count() > 0)
            {
                TempData["MessageError"] = "The Course Name already Existed";
                return RedirectToAction("Update");
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    TempData["MessageError"] = "Can Not Update Course";
                    return RedirectToAction("Update");
                }
                courseInDb.Name = course.Name;
                courseInDb.Description = course.Description;
                courseInDb.CategoryId = course.CategoryId;
                _context.SaveChanges();
            }
            TempData["MessageSuccess"] = "Update Course Successfully";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var courseInDb = _context.Courses.SingleOrDefault(c => c.Id == id);

            if (courseInDb == null)
            {
                TempData["MessageError"] = "The Course doesn't Exist";
                return RedirectToAction("Index");
            }
            var trainer = _context.Trainers.Where(t => t.CourseId == id);
            foreach (var item in trainer)
            {
                item.course = null;
                item.CourseId = null;
            }
            var trainee = _context.Trainees.Where(t => t.CourseId == id);
            foreach (var item in trainee)
            {
                item.course = null;
                item.CourseId = null;

            }
            _context.Courses.Remove(courseInDb);
            _context.SaveChanges();
            TempData["MessageSuccess"] = "Delete Course Successfully";
            return RedirectToAction("Index");
        }

    }
}