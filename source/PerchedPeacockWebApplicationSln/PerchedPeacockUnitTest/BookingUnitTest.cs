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

namespace PerchedPeacockUnitTest
{
    class BookingUnitTest
    {

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


                    BookingController bookingController = new BookingController(context);
                    var location = new Location() { LocationLocality = "Whitefield", LocationBuilding = "TestBuilding" };
                    List<ParkingLot> parkingLots=bookingController.GetFreeParkingSlots(location);

                    Assert.Equal(1, parkingLots.Count);
                }
            }
            finally
            {
                connection.Close();
            }
        }

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


                    BookingController bookingController = new BookingController(context);
                    var location = new Location() { LocationLocality = "Whitefield", LocationBuilding = "TestBuilding" };
                    List<ParkingLot> parkingLots = bookingController.GetAllFreeParkingSlots(location);

                    Assert.Equal(1, parkingLots.Count);
                }
            }
            finally
            {
                connection.Close();
            }
        }
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
                    ParkingLot newParkingLot = new ParkingLot() { ParkingDisplayName = "TestPL", LocationLocality = "Whitefield", LocationBuilding = "TestBuilding", LocationCity = "Bangalore", LocationPinCode = 560000 };
                    await parkingLotController.PostParkingLot(newParkingLot);
                    context.SaveChanges();


                    BookingController bookingController = new BookingController(context);
                    var location = new Location() { Id=1 LocationLocality = "Whitefield", LocationBuilding = "TestBuilding" };
                    var booking = new Booking() { LocationId = location.Id, InTime = DateTime.Now, OutTime = null, IsOccupied = true, ParkingLotId = newParkingLot.Id };
                    List<ParkingLot> parkingLots = bookingController.GetFreeParkingSlots(location);

                    Assert.Equal(1, parkingLots.Count);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        public void TestCalculateBookingCharge()
        {
            BookingUtility bookingUtility = new BookingUtility();
            int bookingCharge=bookingUtility.CalculateBookingCharge(DateTime.Now.AddHours(-2), DateTime.Now, 50);
            Assert.Equal(100, bookingCharge);
            
        }

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
                    var bookingController = new BookingController(context);
                    await TestBookingASlot();
                  bookingController.closeBooking(1);
                    Booking booking = bookingController.getBooking(1);
                    Assert.True(booking.IsOccupied==false);
                }
            }
            finally
            {
                connection.Close();
            }
        }

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
                    var bookingController = new BookingController(context);
                    await TestBookingASlot();                    
                    List<Booking> bookings = bookingController.FindAllBookings();
                    Assert.Equal(1, bookings.Count);
                }
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
