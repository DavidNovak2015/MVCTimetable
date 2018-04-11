using System.Data.Entity;

namespace CLTimeTableDB
{
    public class EntityFramework:DbContext
    {
        public DbSet<CityEntityDL> CityEntityDLTable { get; set; }

        public DbSet<ConnectionEntityDL> ConnectionEntityDLTable { get; set; }

        public DbSet<FeedbackEntityDL> FeedbackEntityDLTable { get; set; }

        public EntityFramework():base("DefaultConnection")
        { }
    }
}
