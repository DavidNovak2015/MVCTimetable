using System.Web.Mvc;
using MVCTimetable.Models;

namespace MVCTimetable.Controllers
{
    public class AdminCitiesController : Controller
    {
        AdminCitiesViewModel adminCitiesViewModel = new AdminCitiesViewModel();

        public ActionResult AddCity()
        {
            return View(adminCitiesViewModel);
        }

        [HttpPost]
        public ActionResult AddCity(AdminCitiesViewModel adminCitiesViewModel)
        {
            if (adminCitiesViewModel.CityName == null)
            {
                return View(adminCitiesViewModel);
            }
            TempData["adminCitiesResult"] = adminCitiesViewModel.AddCity(adminCitiesViewModel);
            return RedirectToAction("Option", "AdminOption");
        }

        [Authorize]
        public ActionResult DisplayCities()
        {
            adminCitiesViewModel.DisplayCities();
            return View(adminCitiesViewModel);
        }

        [Authorize]
        public ActionResult DeleteCities()
        {
            return View(adminCitiesViewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteCities(AdminCitiesViewModel adminCitiesViewModel)
        {
            if (adminCitiesViewModel.IdCity == null)
            {
                return View(adminCitiesViewModel);
            }

            bool found = adminCitiesViewModel.FindCity(adminCitiesViewModel);
            if (!found)
            {
                ModelState.AddModelError(nameof(adminCitiesViewModel.IdCity), $"Die Identifizierungsnummer {adminCitiesViewModel.IdCity} wurde nicht gefunden");
                return View(adminCitiesViewModel);
            }
            return View("DeleteCitiesConfirmation",adminCitiesViewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteCityConfirmed(AdminCitiesViewModel adminCitiesViewModel)
        {
            TempData["adminCitiesResult"] = adminCitiesViewModel.DeleteCity(adminCitiesViewModel);
            return RedirectToAction("Option", "AdminOption");
        }
    }
}