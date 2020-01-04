
using ASPForum.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPForum.Controllers
{
    public class ReplyController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reply
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int categoryId, int threadId, int replyId)
        {
            Thread thread = db.Threads.Include("Subject").FirstOrDefault(thrd => thrd.Id == threadId);
            Reply reply = db.Replies.Include("Author").FirstOrDefault(repl => repl.Id == replyId);
            ViewBag.CategoryId = categoryId;
            ViewBag.ThreadId = thread.Id;
            ViewBag.SubjectId = thread.Subject.Id;
            ViewBag.Reply = reply.Id;
            return View(reply);
        }


        [HttpPut]
        public ActionResult Edit(int categoryId, int subjectId, int threadId, Reply newReply)
        {
            try
            {
                Thread thread = db.Threads.Find(threadId);
                Reply reply = db.Replies.Include("Author").Where(repl => repl.Id == newReply.Id).FirstOrDefault();
                if (reply.Author.Id == User.Identity.GetUserId())
                {
                    reply.Content = newReply.Content;
                    db.SaveChanges();
                }
                else
                {
                    return HttpNotFound();
                }
                return Redirect("/Thread/Show/" + categoryId + "/" + subjectId + "/" + thread.Id);
            }
            catch (Exception e)
            {
                return View();
            }
        }


        [HttpDelete]
        [Authorize(Roles = "User, Administrator, Moderator")]
        public ActionResult Delete(int categoryId, int subjectId, int replyId)
        {
            Reply reply = db.Replies.Include("Author").Include("Thread").FirstOrDefault(repl => repl.Id == replyId);
            if ((reply.Author != null && reply.Author.Id == User.Identity.GetUserId()) || User.IsInRole("Administrator") || User.IsInRole("Moderator"))
            {
                var threadId = reply.Thread.Id;
                db.Replies.Remove(reply);
                db.SaveChanges();
                return Redirect("/Thread/Show/" + categoryId + "/" + subjectId + "/" + threadId);
            }
            else
            {
                return HttpNotFound();
            }
        }



    }
}