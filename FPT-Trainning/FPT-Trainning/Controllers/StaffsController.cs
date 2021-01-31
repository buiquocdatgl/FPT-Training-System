using FPT_Trainning.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPT_Trainning.Controllers
{
    public class StaffsController : Controller
    {
        private ApplicationDbContext _context;
        public StaffsController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Staffs
        public ActionResult Index(string searchInput)
        {
            var staffs = _context.Users
                .Where(u => u.Roles.Any(r => r.RoleId == "2"))
                .ToList();
            if (!searchInput.IsNullOrWhiteSpace())
            {
                staffs = _context.Users
                    .Where(u => u.FullName.Contains(searchInput) && u.Roles.Any(r => r.RoleId == "2"))
                    .ToList();
            }
            return View(staffs);
        }

        public ActionResult Details(string id)
        {
            var staffInDb = _context.Users.SingleOrDefault(u => u.Id == id);
            return View(staffInDb);
        }

        [HttpGet]
        public ActionResult Update(string id)
        {
            var staffInDb = _context.Users.SingleOrDefault(u => u.Id == id);
            if (staffInDb == null)
            {
                return HttpNotFound();
            }
            return View(staffInDb);
        }

        [HttpPost]
        public ActionResult Update(ApplicationUser staff)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var staffInDb = _context.Users.SingleOrDefault(u => u.Id == staff.Id);
            {
                staffInDb.FullName = staff.FullName;
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            var staffInDb = _context.Users.SingleOrDefault(u => u.Id == id);

            if (staffInDb == null) return HttpNotFound();

            _context.Users.Remove(staffInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}