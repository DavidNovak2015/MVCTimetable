using System.Web.Mvc;
using MVCTimetable.Models;

namespace MVCTimetable.Controllers
{
    public class AdminDeleteController : Controller
    {
        [Authorize]
        public ActionResult Delete()
        {
            AdminDeleteViewModel adminDeleteViewModel = new AdminDeleteViewModel();
            return View(adminDeleteViewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Delete(AdminDeleteViewModel adminDeleteViewModel)
        {
             if (!ModelState.IsValid)
            {
                return View(adminDeleteViewModel);
            }
            bool found = adminDeleteViewModel.FindConnection(adminDeleteViewModel.IdConnection);
            if (!found)
            {
                ModelState.AddModelError(nameof(adminDeleteViewModel.IdConnection),$"Die Identifizierungsnummer {adminDeleteViewModel.IdConnection} wurde nicht gefunden.");
                return View(adminDeleteViewModel);
            }
            return View("ConnectionForDelete",adminDeleteViewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteConnection(AdminDeleteViewModel adminDeleteViewModel)
        {
            TempData["adminDeleteResult"] = adminDeleteViewModel.DeleteConnection(adminDeleteViewModel.IdConnection);
            return RedirectToAction("Option","AdminOption");
        }
    }
}