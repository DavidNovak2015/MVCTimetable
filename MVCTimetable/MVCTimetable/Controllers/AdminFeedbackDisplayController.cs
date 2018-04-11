using System.Web.Mvc;
using MVCTimetable.Models;
using CLTimeTableDB;

namespace MVCTimetable.Controllers
{
    public class AdminFeedbackDisplayController : Controller
    {
        [Authorize]
        public ActionResult FeedbackDisplay()
        {
            AdminFeedbackDisplayViewModel adminFeedbackDisplay = new AdminFeedbackDisplayViewModel();
            return View(adminFeedbackDisplay);
        }
    }
}