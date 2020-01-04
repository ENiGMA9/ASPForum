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

        public ActionResult Show(int categoryId, int subjectId, int threadId)
        {
            Thread thread = db.Threads.Include("Author").Include("Replies").Where(thrd => thrd.Id == threadId).FirstOrDefault<Thread>();

            ViewBag.CategoryId = categoryId;
            ViewBag.SubjectId = subjectId;

            //ViewBag.HasThreadDeleteRight = thread.AuthorId == User.Identity.GetUserId() || User.IsInRole("Administrator") || User.IsInRole("Moderator");
            return View(thread);
        }
        


       [HttpDelete]
       [Authorize(Roles = "User, Administrator, Moderator")]
       public ActionResult DeleteReply(int id, int id2) {
            Thread thread = db.Threads.Find(id);
            Reply reply = db.Replies.Find(id2);
            if((reply.Author != null && reply.Author.Id == User.Identity.GetUserId()) || User.IsInRole("Administrator") || User.IsInRole("Moderator")) {
                db.Replies.Remove(reply);
                thread.Replies.Remove(reply);
                db.SaveChanges();
                return Redirect("/Thread/Show/" + thread.Id);
            }
            else {
                return HttpNotFound();
            }
       }


        public ActionResult EditReply(int id, int id2) {
            Thread thread = db.Threads.Find(id);
            Reply reply = db.Replies.Find(id2);
            ViewBag.Thread = thread;
            ViewBag.Reply = reply;
            return View();
        }


        [HttpPut]
        public ActionResult EditReply(int id, int id2, Reply newReply) {
            try {
                Thread thread = db.Threads.Find(id);
                Reply reply = db.Replies.Find(id2);
                if (reply.AuthorId == User.Identity.GetUserId()) {
                    reply.Content = newReply.Content;
                    db.SaveChanges();
                }
                else {
                    return HttpNotFound();
                }
                return Redirect("/Thread/Show/" + thread.Id);
            }catch(Exception e) {
                return View();
            }
        }


        [HttpPost]
        [Authorize(Roles = "User, Administrator, Moderator")]
        public ActionResult AddReply(int categoryId, int threadId, int subjectId, Reply reply)
        {
            try
            {
                Reply actualReply = (Reply)reply;
                actualReply.AuthorId = User.Identity.GetUserId();
                actualReply.Author = db.Users.FirstOrDefault(user => user.Id == actualReply.AuthorId);
                db.Replies.Add(actualReply);
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
