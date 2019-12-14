using ASPForum.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ASPForum.Controllers
{
    public class SubjectController : Controller
    {
        private readonly CategoryDBContext db = new CategoryDBContext();


        public ActionResult Show(int id) {

            Subject subject = db.Subjects.Find(id);
            ViewBag.Subject = subject;
            return View();
        }

    }
}