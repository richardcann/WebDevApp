using Housing.WebAPI.Models;
using Housing.WebAPI.Models.ClientServerDTO;
using Housing.WebAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace HousingTestProj2
{
    public class AppUserServiceTests
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

        [Fact]
        public void AddOperationWritesToDB()
        {
            var options = new DbContextOptionsBuilder<HousingContext>()
                .UseInMemoryDatabase(databaseName: "AddWritesToDB")
                .Options;
            
            // Run the test against one instance of the context
            using (var context = new HousingContext(options))
            {
                var service = new AppUserService(context);
                service.Add(bob);
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new HousingContext(options))
            {
                var rows = context.AppUser.Count();
                Assert.Equal(1, rows);
                Assert.Equal(bob, context.AppUser.Single());
            }
        }

        [Fact]
        public void DeleteOperationRemovesFromDB()
        {
            var options = new DbContextOptionsBuilder<HousingContext>()
                .UseInMemoryDatabase(databaseName: "DeleteDB")
                .Options;
            
            // Run the test against one instance of the context
            using (var context = new HousingContext(options))
            {
                var service = new AppUserService(context);
                var deleted = service.Delete(bob.Username);
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new HousingContext(options))
            {
                var rows = context.AppUser.Count();
                Assert.Equal(0, rows);
            }
        }


        [Fact]
        public async void GetOperationGetsFromDB()
        {
            var options = new DbContextOptionsBuilder<HousingContext>()
                .UseInMemoryDatabase(databaseName: "GetDB")
                .Options;
            
            // Run the test against one instance of the context
            using (var context = new HousingContext(options))
            {
                var service = new AppUserService(context);
                service.Add(bob);
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new HousingContext(options))
            {
                var service = new AppUserService(context);
                var retBob = await service.Get(bob.Username);
                var rows = context.AppUser.Count();
                Assert.Equal(1, rows);
                Assert.Equal(bob, retBob);
            }
        }

        [Fact]
        public async void AuthenticateOperationVerifiesSuccess()
        {
            var options = new DbContextOptionsBuilder<HousingContext>()
                .UseInMemoryDatabase(databaseName: "AuthSuccDB")
                .Options;
            
            // Run the test against one instance of the context
            using (var context = new HousingContext(options))
            {
                var service = new AppUserService(context);
                service.Add(bob);
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new HousingContext(options))
            {
                var service = new AppUserService(context);

                AppUser appUser = await service.Authenticate(new LoginRequest {
                    Username = bob.Username,
                    Password = "password"});

                var rows = context.AppUser.Count();
                Assert.Equal(1, rows);
                Assert.NotNull(appUser);
            }
        }

        [Fact]
        public async void AuthenticateOperationRejectsIncorrect()
        {
            var options = new DbContextOptionsBuilder<HousingContext>()
                .UseInMemoryDatabase(databaseName: "AuthRejDB")
                .Options;
            
            // Run the test against one instance of the context
            using (var context = new HousingContext(options))
            {
                var service = new AppUserService(context);
                service.Add(bob);
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new HousingContext(options))
            {
                var service = new AppUserService(context);

                AppUser appUser = await service.Authenticate(new LoginRequest
                {
                    Username = bob.Username,
                    Password = "notmypassword"
                });

                var rows = context.AppUser.Count();
                Assert.Equal(1, rows);
                Assert.Null(appUser);
            }
        }
    }
}
