using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace gestion.Controllers
{
    public class ResidentController : Controller
    {
        // GET: ResidentController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ResidentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ResidentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ResidentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ResidentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ResidentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ResidentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ResidentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
