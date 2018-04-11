using System;
using System.Collections.Generic;
using System.Linq;
using CLTimeTableDB;
using System.ComponentModel.DataAnnotations;

namespace MVCTimetable.Models
{
    public class ConnectionsViewModel
    {
        CityCache cityCache = new CityCache();
        DbRepository dbRepository = new DbRepository();

        [Display(Name = "Abfahrtsdatum")]
        [Required(ErrorMessage = "Geben Sie bitte das Abfahrtsdatum an")]
        [DataType(DataType.Date)]
        public DateTime? DepartureDate { get; set; }

        [Display(Name ="Abfahrtsstadt")]
        public int DepartureCityId { get; set; }

        [Display(Name ="Anfahrtsstadt")]
        public int ArrivalCityId { get; set; }

        public List<SelectListForWebVisitors> Cities { get; private set; }

        public List<ConnectionEntity> Connections { get; private set; }

        public ConnectionsViewModel()
        {
            Cities = new List<SelectListForWebVisitors>();
            foreach (var city in cityCache.GetCities())
            {
                Cities.Add(new SelectListForWebVisitors(city.Key, city.Value.CityName));
            }           
        }

        public string GetCityName(int cityId)
        {
            CityEntityDL cityEntity = cityCache.GetCityById(cityId);
            string cityName=cityEntity.CityName;
            return cityName;
        }
        private DateTime GetDepartureDate(DateTime? departureDate)
        {
            if (!departureDate.HasValue)
                throw new InvalidOperationException($"{nameof(departureDate)} is empty");

            return departureDate.Value;
        }
        public bool FindConnections(ConnectionsViewModel connectionsViewModel)
        {
            bool found;
            DateTime departureDate = GetDepartureDate(connectionsViewModel.DepartureDate);
            List<ConnectionEntityDL> connectionsFromDB = dbRepository.GetConnectionsByDepartureCityIdArrivalCityId(connectionsViewModel.DepartureCityId, connectionsViewModel.ArrivalCityId);

            if (connectionsFromDB.Count() == 0)
                return found = false;

            Connections = connectionsFromDB.Select(x => new ConnectionEntity(x.DepartureCityId,
                                                                             x.DepartureTime,
                                                                             x.ArrivalCityId,
                                                                             x.ArrivalTime
                                                )).ToList();
                                                { DepartureDate = departureDate; }
                                                ;
            return found = true;
        }
    }
    public class SelectListForWebVisitors
    {
        public int CityId { get; private set; }
        public string CityName { get; private set; }

        public SelectListForWebVisitors(int cityId,string cityName)
        {
            CityId = cityId;
            CityName = cityName;
        }
    }
}