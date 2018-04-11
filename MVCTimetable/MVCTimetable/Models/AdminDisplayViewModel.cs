using System.Collections.Generic;
using System.Linq;
using CLTimeTableDB;

namespace MVCTimetable.Models
{
    public class AdminDisplayViewModel
    {
        DbRepository dbRepository = new DbRepository();
        CityCache cityCache = new CityCache();

        public List<ConnectionEntity> DisplayConnections { get; private set; }
        public AdminDisplayViewModel()
        {
            DisplayConnections = new List<ConnectionEntity>();
            
            List<ConnectionEntityDL> connectionsFromDB = dbRepository.GetConnections();
            DisplayConnections = connectionsFromDB.Select(x => new ConnectionEntity(x.Id, x.DepartureCityId,x.DepartureTime,x.ArrivalCityId,x.ArrivalTime)).ToList();
        }
        public string GetCityById(int placeId)
        {
            CityEntityDL cityEntity=cityCache.GetCityById(placeId);
            string city = cityEntity.CityName;
            return city;
        }
    }
}