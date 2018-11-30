using AutoMapper;
using Housing.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Helpers;
using Xunit;
using System.Linq;
using Housing.WebAPI.Services;
using Housing.WebAPI.Models.InternalDTO;

namespace HousingTestProj2
{
    public class PropertyServiceTests
    {
        private static AppUser bob = new AppUser
        {
            Username = "bb",
            FirstName = "bob",
            LastName = "barker",
            Role = UserRole.Landlord,
            Email = "bob@barker.com",
            PassHash = new byte[] { 86, 52, 101, 110, 84, 99, 83, 102, 114, 68, 78, 114, 110, 74, 65, 52, 74, 73, 66, 109, 90, 69, 89, 90, 82, 99, 99, 67, 105, 88, 120, 83, 71, 104, 97, 80, 78, 90, 56, 109, 75, 101, 103, 61 },
            PassSalt = new byte[] { 170, 226, 88, 55, 3, 10, 240, 92, 41, 41, 120, 243, 49, 222, 110, 181 }
        };

        private static AppUser rob = new AppUser
        {
            Username = "rb",
            FirstName = "rob",
            LastName = "barker",
            Role = UserRole.Landlord,
            Email = "rob@barker.com",
            PassHash = new byte[] { 86, 52, 101, 110, 84, 99, 83, 102, 114, 68, 78, 114, 110, 74, 65, 52, 74, 73, 66, 109, 90, 69, 89, 90, 82, 99, 99, 67, 105, 88, 120, 83, 71, 104, 97, 80, 78, 90, 56, 109, 75, 101, 103, 61 },
            PassSalt = new byte[] { 170, 226, 88, 55, 3, 10, 240, 92, 41, 41, 120, 243, 49, 222, 110, 181 }
        };

        private static Property p1 = new Property
        {
            ID = 100,
            AppUserRef = "rb",
            AddressLine1 = "39 University Road",
            AddressLine2 = "",
            City = "Southamptoon",
            County = "Hampshire",
            Postcode = "SO146UP",
            Latitude = 50.9334486,
            Longitude = -1.3978989,
            PropertyDescription = "2 Toilets!",
            PropertyStatus = Housing.WebAPI.Models.Property.VerificationStatus.Rejected,
            Timestamp = DateTime.Parse("2018-01-01")
        };

        private static Property p1a = new Property
        {
            ID = -1,
            AppUserRef = "rb",
            AddressLine1 = "39 University Road",
            AddressLine2 = "",
            City = "Southampton",
            County = "Hampshire",
            Postcode = "SO146UP",
            Latitude = 50.9334486,
            Longitude = -1.3978989,
            PropertyDescription = "1 Toilet",
            PropertyStatus = Housing.WebAPI.Models.Property.VerificationStatus.Rejected,
            Timestamp = DateTime.Parse("2018-01-01")
        };

        private static Property p1b = new Property
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
            PropertyDescription = "1 Toilet",
            PropertyStatus = Housing.WebAPI.Models.Property.VerificationStatus.Pending,
            Timestamp = DateTime.Parse("2018-01-01")
        };

        private static Property p1bApproved = new Property
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
            PropertyDescription = "1 Toilet",
            PropertyStatus = Housing.WebAPI.Models.Property.VerificationStatus.Approved,
            Timestamp = DateTime.Parse("2018-01-01")
        };

        private static Property p1bRejected = new Property
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
            PropertyDescription = "1 Toilet",
            PropertyStatus = Housing.WebAPI.Models.Property.VerificationStatus.Rejected,
            Timestamp = DateTime.Parse("2018-01-01")
        };

        private static Property p2 = new Property
        {
            ID = 101,
            AppUserRef = "bb",
            AddressLine1 = "30 Alma Road",
            AddressLine2 = "",
            City = "Southampton",
            County = "Hampshire",
            Postcode = "SO146UP",
            Latitude = 50.9221765,
            Longitude = -1.3997237,
            PropertyDescription = "Very nice brickwork",
            PropertyStatus = Housing.WebAPI.Models.Property.VerificationStatus.Approved,
            Timestamp = DateTime.Parse("2018-01-20")
        };

        private static Property p3 = new Property
        {
            ID = 102,
            AppUserRef = "bb",
            AddressLine1 = "22 Portswood Road",
            AddressLine2 = "Portswood",
            City = "Southampton",
            County = "Hampshire",
            Postcode = "SO172ET",
            Latitude = 50.9210701,
            Longitude = -1.3976039,
            PropertyDescription = "It has a roof",
            PropertyStatus = Housing.WebAPI.Models.Property.VerificationStatus.Pending,
            Timestamp = DateTime.Parse("2018-01-29")
        };


        private static Property p4 = new Property
        {
            ID = 103,
            AppUserRef = "bb",
            AddressLine1 = "31 Alma Road",
            AddressLine2 = "",
            City = "Southampton",
            County = "Hampshire",
            Postcode = "SO146UP",
            Latitude = 50.9219074,
            Longitude = -1.4000154,
            PropertyDescription = "Very very nice brickwork",
            PropertyStatus = Housing.WebAPI.Models.Property.VerificationStatus.Approved,
            Timestamp = DateTime.Parse("2018-01-31")
        };

        [Fact]
        public async void AddPropertyToDB()
        {
            var options = new DbContextOptionsBuilder<HousingContext>()
                .UseInMemoryDatabase(databaseName: "AddPropToDB")
                .Options;

            var myProfile = new AutoMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            var mapper = new Mapper(configuration);

            // Run the test against one instance of the context
            using (var context = new HousingContext(options))
            {
                PropertyService ps = new PropertyService(mapper, context);
                await ps.Add(p1);
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new HousingContext(options))
            {
                var rows = context.Property.Count();
                Assert.Equal(1, rows);
                Assert.Equal(p1, context.Property.Single());
            }
        }

        [Fact]
        public async void RemovePropertyFromDB()
        {
            var options = new DbContextOptionsBuilder<HousingContext>()
                .UseInMemoryDatabase(databaseName: "RemovePropFromDB")
                .Options;

            var myProfile = new AutoMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            var mapper = new Mapper(configuration);

            // Run the test against one instance of the context
            using (var context = new HousingContext(options))
            {
                context.Property.Add(p1);
                await context.SaveChangesAsync();
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new HousingContext(options))
            {
                PropertyService ps = new PropertyService(mapper, context);
                ps.Remove(p1.ID);
                var rows = context.Image.Count();
                Assert.Equal(0, rows);
            }
        }

        [Fact]
        public async void GetPropertyFromDB()
        {
            var options = new DbContextOptionsBuilder<HousingContext>()
                .UseInMemoryDatabase(databaseName: "GetPropFromDB")
                .Options;

            var myProfile = new AutoMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            var mapper = new Mapper(configuration);

            // Run the test against one instance of the context
            using (var context = new HousingContext(options))
            {
                context.Property.Add(p1);
                await context.SaveChangesAsync();
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new HousingContext(options))
            {
                PropertyService ps = new PropertyService(mapper, context);
                Property p = await ps.Get(100);
                Assert.Equal(p1, p);
            }
        }

        [Fact]
        public async void UpdatePropertyOnDB()
        {
            var options = new DbContextOptionsBuilder<HousingContext>()
                .UseInMemoryDatabase(databaseName: "UpdatePropOnDB")
                .Options;

            var myProfile = new AutoMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            var mapper = new Mapper(configuration);

            // Run the test against one instance of the context
            using (var context = new HousingContext(options))
            {
                context.Property.Add(p1);
                await context.SaveChangesAsync();
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new HousingContext(options))
            {
                PropertyService ps = new PropertyService(mapper, context);
                ps.Update(100, p1a);
                Assert.Equal(p1b, context.Property.Single());
            }
        }

        [Fact]
        public async void ApprovePropertyStatusDB()
        {
            var options = new DbContextOptionsBuilder<HousingContext>()
                .UseInMemoryDatabase(databaseName: "ApprovePropOnDB")
                .Options;

            var myProfile = new AutoMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            var mapper = new Mapper(configuration);

            // Run the test against one instance of the context
            using (var context = new HousingContext(options))
            {
                context.Property.Add(p1b);
                await context.SaveChangesAsync();
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new HousingContext(options))
            {
                PropertyService ps = new PropertyService(mapper, context);
                ps.Approve(100);
                Assert.Equal(p1bApproved, context.Property.Single());
            }
        }

        [Fact]
        public async void RejectPropertyStatusDB()
        {
            var options = new DbContextOptionsBuilder<HousingContext>()
                .UseInMemoryDatabase(databaseName: "RejectOnDB")
                .Options;

            var myProfile = new AutoMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            var mapper = new Mapper(configuration);

            // Run the test against one instance of the context
            using (var context = new HousingContext(options))
            {
                context.Property.Add(p1bApproved);
                await context.SaveChangesAsync();
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new HousingContext(options))
            {
                PropertyService ps = new PropertyService(mapper, context);
                ps.Reject(100);
                Assert.Equal(p1bRejected, context.Property.Single());
            }
        }
        
        //property by propid
        //myproperties
        //my property with propid

        [Fact]
        public async void GetApprovedPropertiesFromDB()
        {
            var options = new DbContextOptionsBuilder<HousingContext>()
                .UseInMemoryDatabase(databaseName: "ApprovedPropFromDB")
                .Options;

            var myProfile = new AutoMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            // Run the test against one instance of the context
            using (var context = new HousingContext(options))
            {
                context.Property.Add(p1);
                context.Property.Add(p2);
                context.Property.Add(p3);
                context.Property.Add(p4);
                context.AppUser.Add(bob);
                context.AppUser.Add(rob);
                await context.SaveChangesAsync();
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new HousingContext(options))
            {
                PropertyService ps = new PropertyService(mapper, context);
                List<Property> props = mapper.Map<List<BPImages>, List<Property>>(ps.GetProperties(Property.VerificationStatus.Approved).ToList());
               
                Assert.Equal(new List<Property> { p4, p2 }, props);
            }
        }

        [Fact]
        public async void GetPendingPropertiesFromDB()
        {
            var options = new DbContextOptionsBuilder<HousingContext>()
                .UseInMemoryDatabase(databaseName: "PendingPropFromDB")
                .Options;

            var myProfile = new AutoMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            // Run the test against one instance of the context
            using (var context = new HousingContext(options))
            {
                context.Property.Add(p1);
                context.Property.Add(p2);
                context.Property.Add(p3);
                context.Property.Add(p4);
                context.AppUser.Add(bob);
                context.AppUser.Add(rob);
                await context.SaveChangesAsync();
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new HousingContext(options))
            {
                PropertyService ps = new PropertyService(mapper, context);
                List<Property> props = mapper.Map<List<BPImages>, List<Property>>(ps.GetProperties(Property.VerificationStatus.Pending).ToList());

                //In DTO this attribute is not mapped to Property; for assertion
                //purposes setting
                foreach (Property prop in props)
                {
                    prop.PropertyStatus = Property.VerificationStatus.Pending;
                }

                Assert.Equal(new List<Property> { p3 }, props);
            }
        }

        [Fact]
        public async void GetMyPropertiesFromDB()
        {
            var options = new DbContextOptionsBuilder<HousingContext>()
                .UseInMemoryDatabase(databaseName: "MyPropsFromDB")
                .Options;

            var myProfile = new AutoMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            // Run the test against one instance of the context
            using (var context = new HousingContext(options))
            {
                context.Property.Add(p1);
                context.Property.Add(p2);
                context.Property.Add(p3);
                context.Property.Add(p4);
                context.AppUser.Add(bob);
                context.AppUser.Add(rob);
                await context.SaveChangesAsync();
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new HousingContext(options))
            {
                PropertyService ps = new PropertyService(mapper, context);
                List<Property> props = mapper.Map<List<BPIRejections>, List<Property>>(ps.GetMyProperties("bb").ToList());

                Assert.Equal(new List<Property> { p4, p3, p2 }, props);
            }
        }

        [Fact]
        public async void GetPropertyByID()
        {
            var options = new DbContextOptionsBuilder<HousingContext>()
                .UseInMemoryDatabase(databaseName: "PropByIDFromDB")
                .Options;

            var myProfile = new AutoMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            // Run the test against one instance of the context
            using (var context = new HousingContext(options))
            {
                context.Property.Add(p1);
                context.Property.Add(p2);
                context.Property.Add(p3);
                context.Property.Add(p4);
                context.AppUser.Add(bob);
                context.AppUser.Add(rob);
                await context.SaveChangesAsync();
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new HousingContext(options))
            {
                PropertyService ps = new PropertyService(mapper, context);
                Property prop = mapper.Map<BPImages, Property>(ps.GetPropertyByID(100));
                
                //In DTO this attribute is not mapped to Property; for assertion
                //purposes setting
                prop.PropertyStatus = Property.VerificationStatus.Rejected;

                Assert.Equal(p1, prop);
            }
        }

    }
}