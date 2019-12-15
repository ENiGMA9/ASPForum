using ASPForum.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ASPForum.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Show(int id)
        {
            Subject subject = db.Subjects.Find(id);
            ViewBag.Subject = subject;
            ViewBag.NewThreadRight = User.IsInRole("User") || User.IsInRole("Moderator") || User.IsInRole("Administrator");
            var threads = from thread in subject.Threads select thread;
            ViewBag.Threads = threads;
            return View();
        }


        [HttpGet]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult New(int id)
        {
            /*TempData["Category"] = categoryDB.Categories.Find(id);*/
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult New(Subject subject)
        {
            try
            {
                subject.Category = (Category)TempData["Category"];
                db.Subjects.Add(subject);
                db.SaveChanges();
                return Redirect("/Subject/Show/" + subject.Id);
            }
            catch (Exception e)
            {
                // ViewBag.CategoryId = subject.CategoryId;
                return View();
            }
        }

        [HttpGet]
        [Authorize(Roles = "User, Administrator, Moderator")]
        public ActionResult AddThread(int id)
        {
            ViewBag.SubjectId = id;
            ViewBag.UserId = User.Identity.GetUserId();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "User, Administrator, Moderator")]
        public ActionResult AddThread(Thread thread)
        {
            try
            {
                db.Threads.Add(thread);
                db.SaveChanges();
                return Redirect("/Category/Index");
            }
            catch (Exception e)
            {
                // ViewBag.CategoryId = subject.CategoryId;
                return View();
            }
        }
    }
}