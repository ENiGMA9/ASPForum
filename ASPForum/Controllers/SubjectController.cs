using ASPForum.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ASPForum.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Show(int categoryId, int subjectId)
        {
            var categories = from category in db.Categories
                             orderby category.Index
                             select category;
            ViewBag.Categories = categories;
            Subject subject = db.Subjects.Include("Threads").FirstOrDefault(sub => sub.Id == subjectId);
            ViewBag.NewThreadRight = User.IsInRole("User") || User.IsInRole("Moderator") || User.IsInRole("Administrator");
            ViewBag.hasModeratorRight = (User.IsInRole("Administrator") || User.IsInRole("Moderator"));
            ViewBag.categoryId = categoryId;
            return View(subject);
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
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "User, Administrator, Moderator")]
        public ActionResult AddThread(int subjectId, Thread thread)
        {
            try
            {
                var authorId = User.Identity.GetUserId();
                thread.Author = db.Users.FirstOrDefault(user => user.Id == authorId);
                thread.Subject = db.Subjects.Include("Category").FirstOrDefault(sub => sub.Id == subjectId);
                db.Threads.Add(thread);
                db.SaveChanges();
                return Redirect("/Subject/Show/" + thread.Subject.Category.Id + "/" + subjectId);
            }
            catch (Exception e)
            {
                // ViewBag.CategoryId = subject.CategoryId;
                ViewBag.SubjectId = subjectId;
                return View();
            }
        }

        [NonAction]
        public IEnumerable<SelectListItem> GetAllCategories()
        {
            var selectList = new List<SelectListItem>();
            var categories = from category in db.Categories select category;
            foreach (var category in categories)
            {
                selectList.Add(new SelectListItem
                {
                    Value = category.Id.ToString(),
                    Text = category.Name
                });
            }

            return selectList;
        }

        [HttpDelete]
        [Authorize(Roles = ("Administrator, Moderator"))]
        public ActionResult Delete(int id)
        {
            Subject subject = db.Subjects.Find(id);
            db.Subjects.Remove(subject);
            db.SaveChanges();
            return Redirect("/Category/Index");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            ViewBag.Categories = GetAllCategories();
            return View(db.Subjects.FirstOrDefault(sub => sub.Id == id));
        }

        [HttpPut]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(Subject requestSubject, int? categoryId)
        {
            try
            {
                Subject subject = db.Subjects.Find(requestSubject.Id);

                subject.Name = requestSubject.Name;
                subject.Index = requestSubject.Index;
                if (categoryId.HasValue)
                {
                    subject.Category = db.Categories.FirstOrDefault(cat => cat.Id == categoryId);
                }
                
                db.SaveChanges();
                return Redirect("/Category/Index");
            }
            catch (Exception e)
            {
                return View(requestSubject);
            }
        }

    }
}