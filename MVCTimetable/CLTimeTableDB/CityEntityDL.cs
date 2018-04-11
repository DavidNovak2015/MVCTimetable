
namespace CLTimeTableDB
{
    public class CityEntityDL
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public override string ToString()
        {
            return CityName;
        }
    }
}
