using System.ComponentModel.DataAnnotations;

namespace MVCTimetable.Models
{
    public class CityEntity
    {
        [Display(Name ="Identifizierungsnummer")]
        public int Id { get; set; }

        [Display(Name ="Name_der_Stadt")]
        public string CityName { get; set; }

        public CityEntity(int iD,string cityName)
        {
            Id = iD;
            CityName = cityName;
        }
    }
}