using System.ComponentModel.DataAnnotations;
using System;

namespace MVCTimetable.Models
{
    public class ConnectionEntity
    {
        [Display(Name = "Abfahrtsdatum:")]
        public DateTime DepartureDate { get; set; }

        [Display(Name = "Abfahrtsstadt")]
        public int DepartureCityId { get; private set; }

        [Display(Name = "Abfahrtszeit")]
        public TimeSpan DepartureTime { get; private set; }

        [Display(Name = "Anfahrtsstadt")]
        public int ArrivalCityId { get; private set; }

        [Display(Name ="Anfahrtszeit")]
        public TimeSpan ArrivalTime { get; private set; }

        [Display(Name = "Identifizierungsnummer")]
        public int ID { get; private set; }

        public ConnectionEntity(int id, int departureCityId, TimeSpan departureTime, int arrivalCityId, TimeSpan arrivalTime)
        {
            ID = id;
            DepartureCityId = departureCityId;
            DepartureTime = departureTime;
            ArrivalCityId = arrivalCityId;
            ArrivalTime = arrivalTime;
        }
        public ConnectionEntity(int departureCityId, TimeSpan departureTime, int arrivalCityId, TimeSpan arrivalTime)
        {
            DepartureCityId = departureCityId;
            DepartureTime = departureTime;
            ArrivalCityId = arrivalCityId;
            ArrivalTime = arrivalTime;
        }
    }
}