using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;
using Housing.WebAPI.Models;
using Housing.WebAPI.Services;

namespace HousingTests.Testing
{
    public class AppUserServiceTests
    {
        [Fact]
        public void Add_writes_to_database()
        {
            var options = new DbContextOptionsBuilder<HousingContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
                .Options;

            AppUser bob = new AppUser
            {
                Username = "bb",
                FirstName = "bob",
                LastName = "barker",
                Role = UserRole.Landlord,
                Email = "bob@barker.com",
                PassHash = new byte[] { 86, 52, 101, 110, 84, 99, 83, 102, 114, 68, 78, 114, 110, 74, 65, 52, 74, 73, 66, 109, 90, 69, 89, 90, 82, 99, 99, 67, 105, 88, 120, 83, 71, 104, 97, 80, 78, 90, 56, 109, 75, 101, 103, 61 },
                PassSalt = new byte[] { 170, 226, 88, 55, 3, 10, 240, 92, 41, 41, 120, 243, 49, 222, 110, 181 }
            };

            // Run the test against one instance of the context
            using (var context = new HousingContext(options))
            {
                var service = new AppUserService(context);
                service.Add(bob);
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new HousingContext(options))
            {
                Assert.Equal(1, context.AppUser.Count());
                Assert.Equal(bob, context.AppUser.Single());
            }
        }
    }
}