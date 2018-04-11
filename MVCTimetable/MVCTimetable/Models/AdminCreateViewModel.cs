using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CLTimeTableDB;

namespace MVCTimetable.Models
{
    public class AdminCreateViewModel
    {
        CityCache cityCache = new CityCache();

        [Display(Name = "Abfahrtszeit")]
        [Required(ErrorMessage = "Geben Sie bitte die Abfahrtszeit an")]
        public TimeSpan? DepartureTime { get; set; }

        [Display(Name = "Anfahrtszeit")]
        [Required(ErrorMessage = "Geben Sie bitte die Anfahrtszeit an")]
        public TimeSpan? ArrivalTime { get; set; }

        [Display(Name = "Abfahrtsstadt")]
        public int DepartureCityId { get; set; }

        [Display(Name = "Anfahrtsstadt")]
        public int ArrivalCityId { get; set; }

        public List<SelectListForAdmin> Cities { get; private set; }

        public AdminCreateViewModel()
        {           
         Cities = new List<SelectListForAdmin>(); 

            foreach (var city in cityCache.GetCities())
            {
                Cities.Add(new SelectListForAdmin
                (
                    city.Value.CityName,
                    city.Key
                ));
            }
        }

        public TimeSpan GetDepartureTime()
        {
            if (!DepartureTime.HasValue)
                throw new InvalidOperationException($"{nameof(DepartureTime)} is empty.");

            return DepartureTime.Value;
        }
        public TimeSpan GetArrivalTime()
        {
            if (!ArrivalTime.HasValue)
                throw new InvalidOperationException($"{nameof(ArrivalTime)} is empty.");

            return ArrivalTime.Value;
        }

        public string CreateConnection(AdminCreateViewModel adminCreate)
        {
            ConnectionEntityDL connectionToDB = new ConnectionEntityDL(adminCreate.DepartureCityId,
                                                                       adminCreate.GetDepartureTime(),
                                                                       adminCreate.ArrivalCityId,
                                                                       adminCreate.GetArrivalTime()
                                                                      );

            DbRepository dbRepository = new DbRepository();
            return dbRepository.InsertConnection(connectionToDB);
        }
    }
    public class SelectListForAdmin
    {
        public string City { get;  set; }
        public int CityId { get;  set; }
        public SelectListForAdmin(string city, int cityId)
        {
            City = city;
            CityId = cityId;
        }
    }
}