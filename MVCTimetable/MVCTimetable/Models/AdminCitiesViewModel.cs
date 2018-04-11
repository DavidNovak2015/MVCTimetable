using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CLTimeTableDB;

namespace MVCTimetable.Models
{
    public class AdminCitiesViewModel
    {
        DbRepository dbRepository = new DbRepository();
        CityCache cityCache = new CityCache();

        [Display(Name = "Name der Stadt")]
        [Required(ErrorMessage = "Geben Sie bitte einen Namen der Stadt an")]
        public string CityName { get; set; }

        [Display(Name ="Identifizierungsnummer")]
        [Required(ErrorMessage = "Geben Sie bite eine Identifizierungsnummer an")]
        public int? IdCity { get; set; }

        public List<CityEntity> Cities { get; private set; }

        public CityEntity CityToDelete { get; private set; }

        public string AddCity(AdminCitiesViewModel adminCitiesViewModel)
        {
            return dbRepository.AddCity(adminCitiesViewModel.CityName);
        }

        public void DisplayCities()
        {
            if (Cities == null)
                Cities = new List<CityEntity>();

            Cities.Clear();

            Dictionary<int, CityEntityDL> citiesFromDB = cityCache.GetCities();
            Cities = citiesFromDB.Select(x => new CityEntity(x.Key, x.Value.CityName)).ToList();
        }

        private int GetCityId(int? iDCity)
        {
            if (!IdCity.HasValue)
                throw new InvalidOperationException($"{nameof(iDCity)} is empty");

            return iDCity.Value;
        }
        public bool FindCity(AdminCitiesViewModel adminCitiesViewModel)
        {
            bool found;
            int iDCity = GetCityId(adminCitiesViewModel.IdCity);

            CityEntityDL cityFromDB = cityCache.GetCityById(iDCity);
            if (cityFromDB == null)
                return found = false;

            CityToDelete = new CityEntity(cityFromDB.Id, cityFromDB.CityName);
            return found = true;
        }

        public string DeleteCity(AdminCitiesViewModel adminCitiesViewModel)
        {
            int idCity = GetCityId(adminCitiesViewModel.IdCity);
            return dbRepository.DeleteCity(idCity);
        }
    }
}


