using System.Collections.Generic;
using System.Linq;

namespace CLTimeTableDB
{
    public class CityCache
    {
        private Dictionary<int, CityEntityDL> citycache;

        DbRepository dbConnectionRepository = new DbRepository();

        public Dictionary<int, CityEntityDL> GetCities()
        {
            EnsureCacheInited();
            return citycache;
        }

        public CityEntityDL GetCityById(int cityId)
        {
            EnsureCacheInited();
            CityEntityDL city;
            return citycache.TryGetValue(cityId, out city) ? city : null;
        }

        private void EnsureCacheInited()
        {
            if (citycache == null)
            {
                citycache = dbConnectionRepository.GetCities();
            }
        }
    }
}
