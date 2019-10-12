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
using System.Collections.Generic;
using System;
using PerchedPeacockWebApplication.Utility;

namespace PerchedPeacockUnitTest
{
    public class BookingUnitTest
    {
        [Fact]
        public async System.Threading.Tasks.Task TestFindFreeSlotsAsync()
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


                    BookingsController bookingController = new BookingsController(context);
                    var location = new Location() { LocationLocality = "Whitefield", LocationBuilding = "TestBuilding" };
                    var parkingLots = bookingController.GetFreeParkingSlots(location);

                    Assert.Single(parkingLots.Value);
                }
            }
            finally
            {
                connection.Close();
            }
        }
        [Fact]
        public async System.Threading.Tasks.Task TestFindAllFreeSlotsAsync()
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


                    BookingsController bookingController = new BookingsController(context);
                    var location = new Location() { LocationLocality = "Whitefield", LocationBuilding = "TestBuilding" };
                    List<ParkingLot> parkingLots = bookingController.GetAllFreeParkingSlots();

                    Assert.Single(parkingLots);
                }
            }
            finally
            {
                connection.Close();
            }
        }
        [Fact]
        public async System.Threading.Tasks.Task TestBookingASlot()
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
                    var locationController = new LocationsController(context);
                    Location location = new Location() { LocationLocality = "Whitefield", LocationBuilding = "TestBuilding", LocationCity = "Bangalore", LocationPinCode = 560000 };
                    await locationController.PostLocation(location);

                    ParkingLot newParkingLot = new ParkingLot() { ParkingDisplayName = "TestPL", LocationLocality = "Whitefield", LocationBuilding = "TestBuilding", LocationCity = "Bangalore", LocationPinCode = 560000 };
                    await parkingLotController.PostParkingLot(newParkingLot);



                    BookingsController bookingController = new BookingsController(context);
                    await bookingController.PostBooking(newParkingLot.Id);
                    context.SaveChanges();
                    var booking = bookingController.GetBooking(1);

                    Assert.True(booking.Result.Value.IsOccupied == true);
                }
            }
            finally
            {
                connection.Close();
            }
        }
        [Fact]
        public void TestCalculateBookingCharge()
        {
            int bookingCharge = BookingUtility.CalculateBookingCharge(DateTime.Now.AddHours(-2), DateTime.Now, 50);
            Assert.Equal(100, bookingCharge);

        }
        [Fact]
        public async System.Threading.Tasks.Task TestClosingABooking()
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
                    var bookingController = new BookingsController(context);
                    var locationController = new LocationsController(context);
                    var parkingLotController = new ParkingLotsController(context);

                    Location location = new Location() { LocationLocality = "Whitefield", LocationBuilding = "TestBuilding", LocationCity = "Bangalore", LocationPinCode = 560000 };
                    await locationController.PostLocation(location);
                    ParkingLot newParkingLot = new ParkingLot() { ParkingDisplayName = "TestPL", LocationLocality = "Whitefield", LocationBuilding = "TestBuilding", LocationCity = "Bangalore", LocationPinCode = 560000 };
                    await parkingLotController.PostParkingLot(newParkingLot);
                    context.SaveChanges();
                    await bookingController.PostBooking(1);
                    context.SaveChanges();
                    bookingController.CloseBooking(1);
                    context.SaveChanges();
                    var booking = await bookingController.GetBooking(1);
                    Assert.True(booking.Value.IsOccupied == false);
                }
            }
            finally
            {
                connection.Close();
            }
        }
        [Fact]
        public async System.Threading.Tasks.Task TestFindAllBookings()
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
                    var bookingController = new BookingsController(context);
                    var locationController = new LocationsController(context);
                    var parkingLotController = new ParkingLotsController(context);

                    Location location = new Location() { LocationLocality = "Whitefield", LocationBuilding = "TestBuilding", LocationCity = "Bangalore", LocationPinCode = 560000 };
                    await locationController.PostLocation(location);
                    ParkingLot newParkingLot = new ParkingLot() { ParkingDisplayName = "TestPL", LocationLocality = "Whitefield", LocationBuilding = "TestBuilding", LocationCity = "Bangalore", LocationPinCode = 560000 };
                    await parkingLotController.PostParkingLot(newParkingLot);
                    context.SaveChanges();
                    await bookingController.PostBooking(1);
                    context.SaveChanges();
                    bool includeOpenBookings = true;
                    var bookings = await bookingController.GetBooking(includeOpenBookings);
                    Assert.Single(bookings.Value);
                }
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
