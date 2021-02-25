using FPT_Trainning.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPT_Trainning.Controllers
{
    [Authorize(Roles = "STAFF")]
    public class CategoriesController : Controller
    {
        private ApplicationDbContext _context;
        public CategoriesController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index(string searchInput)
        {
            var categories = _context.Categories.ToList();
            if (!searchInput.IsNullOrWhiteSpace())
            {
                categories = _context.Categories
                .Where(c => c.Name.Contains(searchInput) || c.Description.Contains(searchInput))
                .ToList();
            }
            return View(categories);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                TempData["MessageError"] = "Can Not Create Category";
                return View();
            }
            var checkCategory = _context.Categories.Where(c => c.Name == category.Name);
            if (checkCategory.Any())
            {
                TempData["MessageError"] = "The Category Name already Existed";
                return View();
            }
            var newCategory = new Category()
            {
                Name = category.Name,
                Description = category.Description
            };
            _context.Categories.Add(newCategory);

            _context.SaveChanges();
            TempData["MessageSuccess"] = "Create Category Successfully";
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var categoryInDb = _context.Categories.SingleOrDefault(c => c.Id == id);
            if (categoryInDb == null)
            {
                TempData["MessageError"] = "The Category Name doesn't Exist";
                return RedirectToAction("Index");
            }
            return View(categoryInDb);
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var categoryInDb = _context.Categories.SingleOrDefault(c => c.Id == id);
            if (categoryInDb == null)
            {
                TempData["MessageError"] = "The Category does not Exist";
                return RedirectToAction("Index");
            }
            return View(categoryInDb);
        }

        [HttpPost]
        public ActionResult Update(Category category)
        {
            if (category.Name == "")
            {
            TempData["MessageError"] = "Please input the Category Name";
            return RedirectToAction("Update");
            }
            var checkCategory = _context.Categories.Where(c => c.Name == category.Name);
            var categoryInDb = _context.Categories.SingleOrDefault(c => c.Id == category.Id);
            if (categoryInDb == null)
            {
                TempData["MessageError"] = "The Category doesn't Exist";
                return RedirectToAction("Index");
            }
            else if (checkCategory.Any())
            {
                if (categoryInDb.Name != category.Name)
                {
                TempData["MessageError"] = "The Category Name already Existed";
                return View("Update");
                } 
            }
            if (!ModelState.IsValid)
            {
                TempData["MessageError"] = "Can Not Update Category";
                return View();
            }
            categoryInDb.Name = category.Name;
            categoryInDb.Description = category.Description;
            _context.SaveChanges();
            TempData["MessageSuccess"] = "Update Category Successfully";
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var categoryInDb = _context.Categories.SingleOrDefault(c => c.Id == id);

            if (categoryInDb == null)
            {
                TempData["MessageError"] = "The Category doesn't Exist";
                return RedirectToAction("Index");
            }
            var course = _context.Courses.Where(c => c.CategoryId == id);

            if (course.Any())
            {
                TempData["MessageError"] = "The Category Name already Existed  in Course. Can't Delete";
                return RedirectToAction("Index");
            }
            _context.Categories.Remove(categoryInDb);
            _context.SaveChanges();
            TempData["MessageSuccess"] = "Delete Category Successfully";
            return RedirectToAction("Index");
        }

        }
    } 