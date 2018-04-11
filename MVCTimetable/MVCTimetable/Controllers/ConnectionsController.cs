using System.Web.Mvc;
using MVCTimetable.Models;

namespace MVCTimetable.Controllers
{
    public class ConnectionsController : Controller
    {
        public ActionResult DisplayPossibilities()
        {
            ConnectionsViewModel connectionsViewModel = new ConnectionsViewModel();
            if (connectionsViewModel.Cities.Count == 0 ) 
            {
                TempData["NoConnection"] = "Derzeit können wir Ihnen leider keine Verkehrsverbindugen anbieten";
                return RedirectToAction("Index", "Home");
            }
            return View(connectionsViewModel);
        }
        
        public ActionResult DisplayConnections(ConnectionsViewModel connectionsViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("DisplayPossibilities",connectionsViewModel);
            }
            if (connectionsViewModel.DepartureCityId == connectionsViewModel.ArrivalCityId)
            {
                ModelState.AddModelError(nameof(connectionsViewModel.DepartureCityId), "Abfahrtsstad und Anfahrtsstadt sind identisch");
                ModelState.AddModelError(nameof(connectionsViewModel.ArrivalCityId), "Abfahrtsstad und Anfahrtsstadt sind identisch");
                return View("DisplayPossibilities",connectionsViewModel);
            }

            bool found = connectionsViewModel.FindConnections(connectionsViewModel);
            if (!found)
            {
                TempData["NoConnectionThisWay"] = "Derzeit können wir Ihnen in diese Richtung keine VerkehrsVerbindungen anbieten";
                return RedirectToAction("DisplayPossibilities");
            }
            return View(connectionsViewModel);
        }
    }
}