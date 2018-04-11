using System.Web.Mvc;
using MVCTimetable.Models;

namespace MVCTimetable.Controllers
{
    public class AdminFeedbackDeleteController : Controller
    {
        [Authorize]
        public ActionResult FeedbackDelete()
        {
            AdminFeedbackDeleteViewModel adminFeedbackDeleteViewModel = new AdminFeedbackDeleteViewModel();
            return View(adminFeedbackDeleteViewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult FeedbackDelete(AdminFeedbackDeleteViewModel adminFeedbackDeleteViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(adminFeedbackDeleteViewModel);
            }
            bool found = adminFeedbackDeleteViewModel.FindMessage(adminFeedbackDeleteViewModel.Id);
            if (!found)
            {
                ModelState.AddModelError(nameof(adminFeedbackDeleteViewModel.Id), $"Die Identifizierungsnummer {adminFeedbackDeleteViewModel.Id} wurde nicht gefunden");
                return View(adminFeedbackDeleteViewModel);
            }
            return View("MessageForDelete",adminFeedbackDeleteViewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult MessageToDelete(AdminFeedbackDeleteViewModel adminFeedbackDeleteViewModel)
        {
            TempData["adminFeedbackDeleteResult"] = adminFeedbackDeleteViewModel.DeleteMessage(adminFeedbackDeleteViewModel.Id);
            return RedirectToAction("Option","AdminOption");
        }
    }
}