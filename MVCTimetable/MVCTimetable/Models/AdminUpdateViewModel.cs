using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CLTimeTableDB;

namespace MVCTimetable.Models
{
    public class AdminUpdateViewModel
    {
        DbRepository dbRepository = new DbRepository();
        CityCache cityCache = new CityCache();

        [Display(Name ="Identifizierungsnummer")]
        [Required(ErrorMessage ="Geben Sie bitte eine Identifizierungsnummer an")]
        public int? IdConnection { get; set; }

        public List<SelectListForAdmin> Cities { get; private set; }

        public ConnectionEntity DisplayOldConnection { get; private set; }

        public ChangedConnection UpdateConnection { get; set; }

        public AdminUpdateViewModel()
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
        
        public string GetCityById(int cityId)
        {
            CityEntityDL cityEntity=cityCache.GetCityById(cityId);
            string cityName = cityEntity.CityName;
            return cityName;
        }
        private int GetIDConnection(int? idConnection)
        {
            if (!idConnection.HasValue)
                throw new InvalidOperationException($"{nameof(idConnection)} is empty");

            return idConnection.Value;
        }
        public bool FindConnection(int? id)
        {
            int idConnection = GetIDConnection(id);

            bool found;

            ConnectionEntityDL connectionFromDB = dbRepository.GetConnectionByID(idConnection);
            if (connectionFromDB == null)
            {
                return found=false;
            }

            DisplayOldConnection = new ConnectionEntity(idConnection,
                                                         connectionFromDB.DepartureCityId,
                                                         connectionFromDB.DepartureTime,
                                                         connectionFromDB.ArrivalCityId,
                                                         connectionFromDB.ArrivalTime
                                                       );
            
            return found = true; 
        }
        public string ChangeConnection(AdminUpdateViewModel adminUpdateViewModel)
        {
            ConnectionEntityDL connectionToDB = new ConnectionEntityDL(GetIDConnection(adminUpdateViewModel.IdConnection),
                                                                      adminUpdateViewModel.UpdateConnection.DepartureCityId,
                                                                      UpdateConnection.GetDepartureTime(),
                                                                      adminUpdateViewModel.UpdateConnection.ArrivalCityId,
                                                                      UpdateConnection.GetArrivalTime()
                                                                      );
            return dbRepository.UpdateConnection(connectionToDB);
        }
    }
    public class ChangedConnection
    {
        [Display(Name ="Neue Abfahrtszeit")]
        [Required(ErrorMessage = "Geben Sie bitte die neue Abfahrtszeit an")]
        public TimeSpan? DepartureTime { get; set; }

        [Display(Name = "Neue Anfahrtszeit")]
        [Required(ErrorMessage = "Geben Sie bitte die neue Anfahrtszeit an")]
        public TimeSpan? ArrivalTime { get; set; }

        [Display(Name = "Neue Abfahrtsstadt")]
        public int DepartureCityId { get; set; }

        [Display(Name = "Neue Anfahrtsstadt")]
        public int ArrivalCityId { get; set; }

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
    }
}