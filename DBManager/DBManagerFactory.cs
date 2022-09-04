using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Configuration;

namespace DBManager
{
    public class DBManagerContextFactory : IDesignTimeDbContextFactory<DBManagerContext>
    {
        public DBManagerContextFactory() 
        {
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        }

        public DBManagerContext CreateDbContext(string[] args)
        {

            var optionsBuilder = new DbContextOptionsBuilder<DBManagerContext>();
            return new DBManagerContext(optionsBuilder.Options);
        }
    }
}
