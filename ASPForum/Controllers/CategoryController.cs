using ASPForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPForum.Controllers
{
    //[Authorize(Roles = "Administrator")]
    public class CategoryController : Controller
    {

        private CategoryDBContext db = new CategoryDBContext();
        
        public ActionResult Index() {
            var categories = from category in db.Categories
                             orderby category.Index
                             select category;
            ViewBag.Categories = categories;
            return View();
        }

        public ActionResult Show(int id) {
            Category category = db.Categories.Find(id);
            ViewBag.Category = category;
            return View();
        }

        public ActionResult New() {
            return View();
        }

        [HttpPost] 
        public ActionResult New(Category category) {
            try {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }catch (Exception e) {
                return View();
            }
        }

        public ActionResult Edit(int id) {
            Category category = db.Categories.Find(id);
            ViewBag.Category = category;
            return View();
        }

        [HttpPut]
        public ActionResult Edit(int id, Category requestCategory) {
            try {
                Category category = db.Categories.Find(id);
                if (TryUpdateModel(category)) {
                    category.Name = requestCategory.Name;
                    category.Index = requestCategory.Index;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }catch (Exception e) {
                return View();
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id) {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}