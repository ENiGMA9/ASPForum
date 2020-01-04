using ASPForum.Models;
using Microsoft.AspNet.Identity;
using System;
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
        public ActionResult Delete(int threadId, int categoryId, int subjectId)
        {
            try
            {
                Thread thread = db.Threads.Include("Author").Where(thrd=>thrd.Id == threadId).FirstOrDefault();
                if (thread.Author.Id == User.Identity.GetUserId() || User.IsInRole("Administrator") || User.IsInRole("Moderator")) {
                    db.Threads.Remove(thread);
                    db.SaveChanges();
                    return Redirect("/Subject/Show/"+ categoryId + "/" + subjectId);
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

        public ActionResult Show(int categoryId, int subjectId, int threadId)
        {
            Thread thread = db.Threads.Include("Author").Include("Replies").Where(thrd => thrd.Id == threadId).FirstOrDefault<Thread>();

            ViewBag.CategoryId = categoryId;
            ViewBag.SubjectId = subjectId;

            //ViewBag.HasThreadDeleteRight = thread.AuthorId == User.Identity.GetUserId() || User.IsInRole("Administrator") || User.IsInRole("Moderator");
            return View(thread);
        }
        


        [HttpPost]
        [Authorize(Roles = "User, Administrator, Moderator")]
        public ActionResult AddReply(int categoryId, int threadId, int subjectId, Reply reply)
        {
            try
            {
                var authorId = User.Identity.GetUserId();
                reply.Author = db.Users.FirstOrDefault(user => user.Id == authorId);
                reply.Thread = db.Threads.FirstOrDefault(thrd => thrd.Id == threadId);
                db.Replies.Add(reply);
                db.SaveChanges();
                return Redirect("/Thread/Show/" + categoryId + "/" + subjectId + "/" + threadId);
            }
            catch (Exception e)
            {
                // ViewBag.CategoryId = subject.CategoryId;
                return View();
            }
        }
    }
}
