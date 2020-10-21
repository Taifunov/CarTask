using CarTestTask.Tests.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace CarTestTask.Tests.DbContext
{
    public static class DbContextMocker
    {
        public static DataContext GetCarsDbContext(string dbName)
        {
            // Create options for DbContext instance
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            // Create instance of DbContext
            var dbContext = new DataContext(options);

            // Add entities in memory
            dbContext.Seed();

            return dbContext;
        }
    }
}
