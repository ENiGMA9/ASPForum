
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


        [HttpDelete]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Delete(int id) {
            Reply reply = db.Replies.Find(id);
            if (reply.Author.Id == User.Identity.GetUserId() || User.IsInRole("Administrator") || User.IsInRole("Moderator")) {
                db.Replies.Remove(reply);
                db.SaveChanges();
            }
            return Redirect("/Category/Index");
        }



    }
}