using System.Web.Mvc;
using MVCTimetable.Models;

namespace MVCTimetable.Controllers
{
    public class AdminDisplayController : Controller
    {
        [Authorize]
        public ActionResult Display()
        {
            AdminDisplayViewModel adminDisplayViewModel = new AdminDisplayViewModel();
            return View(adminDisplayViewModel);
        }
    }
}