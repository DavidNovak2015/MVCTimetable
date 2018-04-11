using System;
using System.ComponentModel.DataAnnotations;
using CLTimeTableDB;

namespace MVCTimetable.Models
{
    public class AdminDeleteViewModel
    {
        DbRepository dbRepository = new DbRepository();
        CityCache cityCache = new CityCache();

        [Display(Name = "Identifizierungsnummer")]
        [Required(ErrorMessage = "Geben Sie bitte eine Identifizierungsnummer an")]
        public int? IdConnection { get; set; }

        public ConnectionEntity ConnectionToDelete { get; private set; }


        public string GetCityById(int cityId)
        {
            CityEntityDL cityEntity = cityCache.GetCityById(cityId);
            string cityName = cityEntity.CityName;
            return cityName;
        }
        private int GetIdConnection(int? idConnection)
        {
            if (!idConnection.HasValue)
                throw new InvalidOperationException($"{nameof(idConnection)} is empty.");

            return idConnection.Value;
        }

        public bool FindConnection(int? id)
        {
            int idConnection = GetIdConnection(id); 
            bool found;
            ConnectionEntityDL connectionFromDB = dbRepository.GetConnectionByID(idConnection);
            if (connectionFromDB == null)
            {
                return found = false;
            }
            ConnectionToDelete = new ConnectionEntity(connectionFromDB.Id,
                                                      connectionFromDB.DepartureCityId,
                                                      connectionFromDB.DepartureTime,
                                                      connectionFromDB.ArrivalCityId,
                                                      connectionFromDB.ArrivalTime
                                                   );
            return found = true;
        }
        public string DeleteConnection(int? id)
        {
            int idConnection = GetIdConnection(id);
            return dbRepository.DeleteConnection(idConnection);
        }    
    }
}