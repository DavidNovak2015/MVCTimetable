using System;

namespace CLTimeTableDB
{
    public class ConnectionEntityDL
    {
        public int Id { get; private set; }

        public int DepartureCityId { get; private set; }

        public TimeSpan DepartureTime { get; private set; }


        public int ArrivalCityId { get; private set; }

        public TimeSpan ArrivalTime { get; private set; }

        public ConnectionEntityDL(int id, int departureCityId, TimeSpan departureTime, int arrivalCityId, TimeSpan arrivalTime)
        {
            Id = id;
            DepartureCityId = departureCityId;
            DepartureTime = departureTime;
            ArrivalCityId = arrivalCityId;
            ArrivalTime = arrivalTime;
        }
        public ConnectionEntityDL(int departureCityId, TimeSpan departureTime, int arrivalCityId, TimeSpan arrivalTime)
        {
            DepartureCityId = departureCityId;
            DepartureTime = departureTime;
            ArrivalCityId = arrivalCityId;
            ArrivalTime = arrivalTime;
        }
        public ConnectionEntityDL()
        { }
        public ConnectionEntityDL(int iD)
        {
            Id = iD;
        }
    }
}
