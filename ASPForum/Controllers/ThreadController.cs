using ASPForum.Models;
using System.Web.Mvc;

namespace ASPForum.Controllers
{
    public class ThreadController : Controller
    {
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
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Delete(int id)
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
    }
}
