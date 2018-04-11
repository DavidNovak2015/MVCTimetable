using System.Web.Mvc;
using MVCTimetable.Models;

namespace MVCTimetable.Controllers
{
    
    public class AdminUpdateController : Controller
    {
        [Authorize]
        public ActionResult Update()
        {
            AdminUpdateViewModel adminUpdateViewModel = new AdminUpdateViewModel();
            return View(adminUpdateViewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Update(AdminUpdateViewModel adminUpdateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(adminUpdateViewModel);
            }
            bool found = adminUpdateViewModel.FindConnection(adminUpdateViewModel.IdConnection);
            if (!found)
            {
                ModelState.AddModelError(nameof(adminUpdateViewModel.IdConnection), $"Die Identifizierungsnummer {adminUpdateViewModel.IdConnection} wurde nicht gefunden");
                return View(adminUpdateViewModel);
            }
            return View("ConnectionForUpdate", adminUpdateViewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult UpdateConnection(AdminUpdateViewModel adminUpdateViewModel)
        {
            if (adminUpdateViewModel.UpdateConnection.DepartureTime == null || adminUpdateViewModel.UpdateConnection.ArrivalTime == null)
            {
                TempData["MistakeAdminUpdate"] = "Neue Angaben wurden nicht ausgefüllt.Versuchen Sie es noch einmal.";
                return RedirectToAction("Option","AdminOption");
            }
            TempData["adminUpdate"]= adminUpdateViewModel.ChangeConnection(adminUpdateViewModel);
            return RedirectToAction("Option", "AdminOption");
        }
    }
}