using System.Web.Mvc;
using MVCTimetable.Models;

namespace MVCTimetable.Controllers
{
    public class AdminCreateController : Controller
    {
        [Authorize]
        public ActionResult Create()
        {
            AdminCreateViewModel adminCreateViewModel = new AdminCreateViewModel();
            return View(adminCreateViewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(AdminCreateViewModel adminCreateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(adminCreateViewModel);
            }
            TempData["adminResult"] = adminCreateViewModel.CreateConnection(adminCreateViewModel);
            return RedirectToAction("Create");
        }
    }
}