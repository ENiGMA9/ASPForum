﻿using ASPForum.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ASPForum.Controllers
{
    public class CategoryController : Controller
    {

        private readonly CategoryDBContext db = new CategoryDBContext();
        
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
            Category category = db.Categories.Find(id);
            ViewBag.Category = category;
            return View();
        }

        [HttpPut]
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id) {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}