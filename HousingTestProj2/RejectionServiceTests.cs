using AutoMapper;
using Housing.WebAPI.Models;
using Housing.WebAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Helpers;
using System.Linq;
using Xunit;

namespace HousingTestProj2
{
    public class RejectionServiceTests
    {
        private static Rejection r1 = new Rejection
        {
            ID = 1,
            PropertyRef = 100,
            Description = "Please add at least one photo",
            Timestamp = DateTime.Parse("2018-02-01")
        };

        private static Rejection r2 = new Rejection
        {
            ID = 2,
            PropertyRef = 101,
            Description = "Inaccurate description",
            Timestamp = DateTime.Parse("2018-01-01")
        };

        private static Rejection r3 = new Rejection
        {
            ID = 3,
            PropertyRef = 100,
            Description = "PLEASE add at least one photo",
            Timestamp = DateTime.Parse("2018-01-02")
        };
        
        private static Property propPend = new Property
        {
            ID = 100,
            AppUserRef = "rb",
            AddressLine1 = "39 University Road",
            AddressLine2 = "",
            City = "Southampton",
            County = "Hampshire",
            Postcode = "SO146UP",
            Latitude = 50.9334486,
            Longitude = -1.3978989,
            PropertyDescription = "2 Toilets!",
            PropertyStatus = Housing.WebAPI.Models.Property.VerificationStatus.Pending,
            Timestamp = DateTime.Parse("2018-01-02")
        };

        private static DateTime rejectionTime = DateTime.Parse("2018-01-02");

        private static Property propReject = new Property
        {
            ID = 100,
            AppUserRef = "rb",
            AddressLine1 = "39 University Road",
            AddressLine2 = "",
            City = "Southampton",
            County = "Hampshire",
            Postcode = "SO146UP",
            Latitude = 50.9334486,
            Longitude = -1.3978989,
            PropertyDescription = "2 Toilets!",
            PropertyStatus = Housing.WebAPI.Models.Property.VerificationStatus.Pending,
            Timestamp = rejectionTime
        };
        
        [Fact]
        public async void AddOperationWritesToDB()
        {
            var options = new DbContextOptionsBuilder<HousingContext>()
                .UseInMemoryDatabase(databaseName: "AddRejectionToDB")
                .Options;
            

            var myProfile = new AutoMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            var mapper = new Mapper(configuration);

            // Run the test against one instance of the context
            using (var context = new HousingContext(options))
            {
                RejectionService rs = new RejectionService(mapper, context);
                await rs.Add(r1);
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new HousingContext(options))
            {
                var rows = context.Rejection.Count();
                Assert.Equal(1, rows);
                Assert.Equal(r1, context.Rejection.Single());
            }
        }
        [Fact]
        public async void RemoveRejectionsRemovesFromDB()
        {
            var options = new DbContextOptionsBuilder<HousingContext>()
                .UseInMemoryDatabase(databaseName: "RemoveRejectionsFromDB")
                .Options;
            

            var myProfile = new AutoMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            var mapper = new Mapper(configuration);

            // Run the test against one instance of the context
            using (var context = new HousingContext(options))
            {
                context.Rejection.Add(r1);
                context.Rejection.Add(r2);
                context.Rejection.Add(r3);
                await context.SaveChangesAsync();
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new HousingContext(options))
            {
                RejectionService rs = new RejectionService(mapper, context);
                rs.RemovePropertyRejections(100);
                var rows = context.Rejection.Count();
                Assert.Equal(r2, context.Rejection.Single());
            }
        }

        [Fact]
        public async void GetRejectionsGetsFromDB()
        {
            var options = new DbContextOptionsBuilder<HousingContext>()
                .UseInMemoryDatabase(databaseName: "GetRejectionsFromDB")
                .Options;
            ;

            var myProfile = new AutoMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            var mapper = new Mapper(configuration);

            // Run the test against one instance of the context
            using (var context = new HousingContext(options))
            {
                context.Rejection.Add(r1);
                context.Rejection.Add(r2);
                context.Rejection.Add(r3);
                await context.SaveChangesAsync();
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new HousingContext(options))
            {
                RejectionService rs = new RejectionService(mapper, context);
                var x = rs.GetRejections(100);
                Assert.Equal( new List<Rejection> { r1, r3 }, x );
            }
        }

    }
}
