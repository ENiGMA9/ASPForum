using ASPForum.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ASPForum.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index() {
            var categories = from category in db.Categories
                             orderby category.Index
                             select category;
            ViewBag.Categories = categories;
            ViewBag.hasRights = User.IsInRole("Administrator");
            return View();
        }

        public ActionResult Show(int id) {
            Category category = db.Categories.Find(id);
            var subjects = from subject in category.Subjects select subject;
            ViewBag.Subjects = subjects; 
            return View(category); 
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult New() {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult New(Category category) {
            try {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }catch (Exception e) {
                return View();
            }
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id) {
            return View(db.Categories.Find(id));
        }

        [HttpPut]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(Category requestCategory) {
            try {
                if (ModelState.IsValid)
                {
                    Category category = db.Categories.Find(requestCategory.Id);
                    if (TryUpdateModel(category))
                    {
                        category.Name = requestCategory.Name;
                        category.Index = requestCategory.Index;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(requestCategory);
                }
            }catch (Exception e) {
                return View();
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id) {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult AddSubject(int id)
        {
            ViewBag.CategoryId = id;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult AddSubject(int categoryId, Subject subject)
        {
            try
            {
                subject.Category = db.Categories.FirstOrDefault(cat => cat.Id == categoryId);
                db.Subjects.Add(subject);
                db.SaveChanges();
                return Redirect("/Subject/Show/"+categoryId + "/" + subject.Id);
            }
            catch (Exception e)
            {
                return View();
            }
        }
    }
}