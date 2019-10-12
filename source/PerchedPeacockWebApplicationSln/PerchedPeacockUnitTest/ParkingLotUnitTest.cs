using Xunit;
using Moq;
using PerchedPeacockWebApplication.Models;
using PerchedPeacockWebApplication.Controllers;
using PerchedPeacockWebApplication.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using IdentityServer4.EntityFramework.Options;
using Microsoft.Extensions.Options;

namespace PerchedPeacockUnitTest
{
    public class ParkingLotUnitTest
    {
        [Fact]
        public async System.Threading.Tasks.Task TestCreateParkingLotByIdAsync()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            try
            {
                var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseSqlite(connection)
                   .Options;

                var operationalStoreOptionsMock = new Mock<OperationalStoreOptions>();
                operationalStoreOptionsMock.Object.ConfigureDbContext = dbContext => dbContext.UseSqlite(connection);
                var iOptionsMock = Options.Create<OperationalStoreOptions>(operationalStoreOptionsMock.Object);

                // Create the schema in the database
                using (var context = new ApplicationDbContext(options, iOptionsMock))
                {
                    context.Database.EnsureCreated();
                }

                // Run the test against one instance of the context
                using (var context = new ApplicationDbContext(options, iOptionsMock))
                {
                    var parkingLotController = new ParkingLotsController(context);
                    ParkingLot newParkingLot = new ParkingLot() { ParkingDisplayName = "TestPL", LocationLocality = "Whitefield", LocationBuilding = "TestBuilding", LocationCity = "Bangalore", LocationPinCode = 560000 };
                    await parkingLotController.PostParkingLot(newParkingLot);
                    context.SaveChanges();
                    //Assert.Equal(1, parkingLotObj.Id);
                }


                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new ApplicationDbContext(options, iOptionsMock))
                {
                    Assert.Equal(1, context.ParkingLot.Count());
                    Assert.Equal("Whitefield", context.ParkingLot.Single().LocationLocality);
                }
            }
            finally
            {
                connection.Close();
            }
        }


        [Fact]
        public void TestUpdateParkingLot()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            try
            {
                var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseSqlite(connection)
                   .Options;

                var operationalStoreOptionsMock = new Mock<OperationalStoreOptions>();
                operationalStoreOptionsMock.Object.ConfigureDbContext = dbContext => dbContext.UseSqlite(connection);
                var iOptionsMock = Options.Create<OperationalStoreOptions>(operationalStoreOptionsMock.Object);

                // Create the schema in the database
                using (var context = new ApplicationDbContext(options, iOptionsMock))
                {
                    context.Database.EnsureCreated();
                }

                // Run the test against one instance of the context
                using (var context = new ApplicationDbContext(options, iOptionsMock))
                {

                    var parkingLotController = new ParkingLotsController(context);
                    ParkingLot updatedParkingLot = new ParkingLot() {Id=1,  ParkingDisplayName = "NewName", LocationLocality = "Whitefield", LocationBuilding = "TestBuilding", LocationCity = "Bangalore", LocationPinCode = 560000 };
                    var parkingLotObjUpdated = parkingLotController.PutParkingLot(1, updatedParkingLot);
                    var getParkingLotObj = parkingLotController.GetParkingLot(1);
                    Assert.Equal(updatedParkingLot.ParkingDisplayName, getParkingLotObj.Result.Value.ParkingDisplayName);
                }
            }
            finally
            {
                connection.Close();
            }

        }
    }
}
