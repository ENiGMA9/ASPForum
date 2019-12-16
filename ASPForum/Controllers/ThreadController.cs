using ASPForum.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace ASPForum.Controllers
{
    public class ThreadController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(Thread thread)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPut]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Edit(int id, Thread requestThread)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpDelete]
        [Authorize(Roles = "User, Administrator, Moderator")]
        public ActionResult Delete(int id)
        {
            try
            {
                Thread thread = db.Threads.Find(id);
                if (thread.AuthorId == User.Identity.GetUserId() || User.IsInRole("Administrator") || User.IsInRole("Moderator")) {
                    db.Threads.Remove(thread);
                    db.SaveChanges();
                    return Redirect("/Category/Index");
                }
                else {
                    return HttpNotFound();
                }
                
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Show(int id)
        {
            Thread thread = db.Threads.Find(id);
            ViewBag.Thread = thread;

            // Hack to fix old DB inserts
            if(thread.Author == null && thread.AuthorId != null)
            {
                if (TryUpdateModel(thread))
                {
                    thread.Author = db.Users.FirstOrDefault(user => user.Id == thread.AuthorId);
                    db.SaveChanges();
                }
            }

            ViewBag.Replies = from reply in thread.Replies select reply;
            //ViewBag.HasThreadDeleteRight = thread.AuthorId == User.Identity.GetUserId() || User.IsInRole("Administrator") || User.IsInRole("Moderator");
            return View();
        }

    }
}
