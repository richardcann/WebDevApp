using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Housing.WebAPI.Models
{
    public class HousingContext : DbContext
    {
        public HousingContext(DbContextOptions<HousingContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=wevdevhousing3.database.windows.net;Database=Housing;Trusted_Connection=False;User ID=webdevadmin;Password=rezzaIsTheBabe69;Encrypt=True;");
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Property>()
                .HasIndex(p => new { p.AddressLine1, p.Postcode })
                .IsUnique();

            modelBuilder.Entity<AppUser>()
                .HasData(
                        new AppUser { Username = "bb", FirstName = "bob", LastName = "barker", Role = UserRole.Landlord, Email = "bob@barker.com", PassHash = new byte[] { 86, 52, 101, 110, 84, 99, 83, 102, 114, 68, 78, 114, 110, 74, 65, 52, 74, 73, 66, 109, 90, 69, 89, 90, 82, 99, 99, 67, 105, 88, 120, 83, 71, 104, 97, 80, 78, 90, 56, 109, 75, 101, 103, 61 }, PassSalt = new byte[] { 170, 226, 88, 55, 3, 10, 240, 92, 41, 41, 120, 243, 49, 222, 110, 181 } },
                        new AppUser { Username = "rb", FirstName = "rob", LastName = "barker", Role = UserRole.Landlord, Email = "rob@barker.com", PassHash = new byte[] { 100, 49, 87, 52, 73, 75, 117, 102, 114, 87, 48, 51, 80, 113, 66, 85, 43, 81, 115, 56, 73, 116, 107, 113, 85, 53, 108, 99, 82, 99, 106, 84, 66, 120, 52, 117, 121, 83, 68, 78, 90, 52, 89, 61 }, PassSalt = new byte[] { 220, 11, 92, 219, 13, 121, 119, 37, 204, 189, 166, 18, 64, 216, 146, 0 } },

                        new AppUser { Username = "jennaj", FirstName = "jenna", LastName = "jenkins", Role = UserRole.Officer, Email = "jenna@accomodationservices.soton.ac.uk", PassHash = new byte[] { 73, 116, 53, 112, 57, 119, 98, 86, 98, 54, 65, 102, 73, 90, 115, 107, 100, 110, 53, 98, 87, 101, 97, 66, 105, 97, 120, 57, 71, 75, 114, 83, 106, 49, 53, 115, 119, 48, 102, 66, 48, 83, 77, 61 }, PassSalt = new byte[] { 83, 62, 209, 222, 65, 42, 68, 22, 67, 179, 195, 6, 86, 173, 17, 83 } },

                        new AppUser { Username = "james1", FirstName = "james", LastName = "jones", Role = UserRole.Student, Email = "jj18g12@soton.ac.uk", PassHash = new byte[] { 118, 73, 98, 118, 80, 112, 107, 111, 112, 73, 89, 98, 52, 121, 97, 110, 71, 114, 76, 55, 86, 114, 69, 70, 50, 84, 49, 104, 101, 117, 118, 73, 121, 56, 120, 47, 90, 65, 100, 84, 105, 76, 111, 61 }, PassSalt = new byte[] { 87, 249, 212, 34, 178, 242, 73, 89, 190, 134, 198, 45, 104, 247, 147, 172 } },
                        new AppUser { Username = "lucy34", FirstName = "lucy", LastName = "smith", Role = UserRole.Student, Email = "ls1@solent.ac.uk", PassHash = new byte[] { 117, 88, 75, 84, 66, 90, 108, 69, 71, 66, 85, 86, 74, 86, 111, 107, 104, 111, 68, 81, 81, 73, 72, 85, 55, 99, 109, 102, 68, 83, 79, 99, 119, 112, 49, 56, 83, 66, 87, 65, 106, 73, 119, 61 }, PassSalt = new byte[] { 78, 58, 118, 152, 181, 95, 9, 193, 58, 232, 249, 10, 221, 233, 18, 205 } },
                        new AppUser { Username = "sarah-X", FirstName = "sarah", LastName = "xavier", Role = UserRole.Student, Email = "sx9g17@soton.ac.uk", PassHash = new byte[] { 117, 80, 121, 117, 108, 55, 85, 103, 84, 99, 98, 68, 67, 55, 106, 106, 98, 83, 105, 86, 73, 117, 113, 70, 50, 90, 84, 97, 111, 67, 113, 111, 74, 108, 84, 84, 75, 105, 55, 57, 76, 79, 89, 61 }, PassSalt = new byte[] { 3, 41, 221, 8, 163, 31, 47, 119, 131, 156, 1, 36, 73, 117, 21, 179 } },
                        new AppUser { Username = "alexthefantastic", FirstName = "alexander", LastName = "davies", Role = UserRole.Student, Email = "ad12g17@ecs.soton.ac.uk", PassHash = new byte[] { 103, 113, 97, 74, 116, 69, 65, 117, 110, 88, 75, 109, 117, 88, 66, 71, 80, 104, 81, 122, 43, 73, 110, 67, 51, 121, 102, 69, 112, 55, 87, 88, 99, 74, 113, 105, 48, 67, 85, 98, 65, 101, 85, 61 }, PassSalt = new byte[] { 124, 12, 164, 118, 20, 110, 205, 123, 64, 63, 142, 56, 140, 40, 208, 181 } },
                        new AppUser { Username = "jo92", FirstName = "jo", LastName = "strong", Role = UserRole.Student, Email = "js4@solent.ac.uk", PassHash = new byte[] { 85, 72, 65, 50, 110, 118, 100, 84, 69, 90, 117, 83, 107, 55, 111, 68, 76, 119, 109, 113, 76, 71, 55, 75, 84, 49, 97, 100, 120, 105, 109, 43, 118, 53, 97, 106, 97, 72, 105, 116, 114, 114, 69, 61 }, PassSalt = new byte[] { 33, 190, 184, 105, 243, 213, 124, 181, 10, 5, 221, 74, 56, 130, 26, 203 } }
                    );

            modelBuilder.Entity<Property>().HasData(
                        new Property { ID = 100, AppUserRef = "rb", AddressLine1 = "39 University Road", AddressLine2 = "", City = "Southampton", County = "Hampshire", Postcode = "SO146UP", Latitude = 50.9334486, Longitude = -1.3978989, PropertyDescription = "2 Toilets!", PropertyStatus = Models.Property.VerificationStatus.Rejected, Timestamp = DateTime.Parse("2018-01-01") },
                        new Property { ID = 101, AppUserRef = "bb", AddressLine1 = "30 Alma Road", AddressLine2 = "", City = "Southampton", County = "Hampshire", Postcode = "SO146UP", Latitude = 50.9221765, Longitude = -1.3997237, PropertyDescription = "Very nice brickwork", PropertyStatus = Models.Property.VerificationStatus.Approved, Timestamp = DateTime.Parse("2018-01-20") },
                        new Property { ID = 102, AppUserRef = "bb", AddressLine1 = "22 Portswood Road", AddressLine2 = "Portswood", City = "Southampton", County = "Hampshire", Postcode = "SO172ET", Latitude = 50.9210701, Longitude = -1.3976039, PropertyDescription = "It has a roof", PropertyStatus = Models.Property.VerificationStatus.Pending, Timestamp = DateTime.Parse("2018-01-29") },
                        new Property { ID = 103, AppUserRef = "bb", AddressLine1 = "31 Alma Road", AddressLine2 = "", City = "Southampton", County = "Hampshire", Postcode = "SO146UP", Latitude = 50.9219074, Longitude = -1.4000154 , PropertyDescription = "Very very nice brickwork", PropertyStatus = Models.Property.VerificationStatus.Approved, Timestamp = DateTime.Parse("2018-01-31") }
                        );

            modelBuilder.Entity<Image>().HasData(
                        new Image { ID = 1, PropertyRef = 101, Position = 1, Path = "1aa7f469-72fb-4f9b-9861-cd5445dd3486.jpeg" },
                        new Image { ID = 2, PropertyRef = 101, Position = 2, Path = "2a757997-283f-4e32-95af-71460a81c87f.jpeg" },
                        new Image { ID = 3, PropertyRef = 101, Position = 3, Path = "2db635bf-6d9f-45db-8da9-837f2cea0db8.jpeg" },

                        new Image { ID = 4, PropertyRef = 102, Position = 1, Path = "1ea369fe-8059-44f6-8b48-b17211d8d26b.jpeg" },
                        new Image { ID = 5, PropertyRef = 102, Position = 2, Path = "3aa72703-a50a-40c0-9871-a85cb78837c8.jpeg" }
                    );

            modelBuilder.Entity<Rejection>().HasData(
                         new Rejection { ID = 1, PropertyRef = 100, Description = "Please add at least one photo", Timestamp = DateTime.Parse("2018-02-01") }
                    );

        }


        public DbSet<AppUser> AppUser { get; set; }

        public DbSet<Image> Image { get; set; }

        public DbSet<Property> Property { get; set; }

        public DbSet<Rejection> Rejection { get; set; }
    }
}
