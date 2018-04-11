using System.Web.Mvc;

namespace MVCTimetable.Controllers
{
    public class AdminOptionController : Controller
    {
        [Authorize]
        public ActionResult Option()
        {
            return View();
        }
    }
}