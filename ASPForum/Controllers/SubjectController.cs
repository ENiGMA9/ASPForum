using ASPForum.Models;
using System;
using System.Web.Mvc;

namespace ASPForum.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Show(int id) {

            Subject subject = db.Subjects.Find(id);
            ViewBag.Subject = subject;
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

    }
}