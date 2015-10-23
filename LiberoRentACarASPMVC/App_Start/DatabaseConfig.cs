using LiberoRentACarPersistence;
using System.Data.Entity;

namespace LiberoRentACarASPMVC
{
    public static class DatabaseConfig
    {
        public static void Initialize()
        {
            using (LocadoraContext ctx = new LocadoraContext())
            {
                Database.SetInitializer<LocadoraContext>(new CustomDropDatabaseIfModelChanges());
                //ctx.Database.Initialize(false);
            }
        }
    }
}