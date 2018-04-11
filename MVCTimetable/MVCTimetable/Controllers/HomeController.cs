using System.Web.Mvc;
using MVCTimetable.Models;

namespace MVCTimetable.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            FeedbackViewModel feedbackViewModel = new FeedbackViewModel();
            return View(feedbackViewModel);
        }
        
        [HttpPost]
        public ActionResult Index(FeedbackViewModel feedbackViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(feedbackViewModel);
            }
            TempData["FeedbackResult"] = feedbackViewModel.AddMessageToDatabase(feedbackViewModel);
            return RedirectToAction("Index");
        }
    }
}