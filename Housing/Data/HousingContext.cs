using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Housing.Models;

namespace Housing.Models
{
    public class HousingContext : DbContext
    {
        public HousingContext (DbContextOptions<HousingContext> options)
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

            modelBuilder.Entity<AppUser>().HasData(
                        new AppUser { Username = "bb", FirstName = "bob", LastName = "barker", Role = UserRole.Landlord, Email = "bob@barker.com", PassHash = Enumerable.Repeat((byte)0x20, 8).ToArray(), PassSalt = Enumerable.Repeat((byte)0x20, 8).ToArray() },
                        new AppUser { Username = "rb", FirstName = "rob", LastName = "barker", Role = UserRole.Landlord, Email = "rob@barker.com", PassHash = Enumerable.Repeat((byte)0x20, 8).ToArray(), PassSalt = Enumerable.Repeat((byte)0x20, 8).ToArray() },

                        new AppUser { Username = "jennaj", FirstName = "jenna", LastName = "jenkins", Role = UserRole.Officer, Email = "jenna@accomodationservices.soton.ac.uk", PassHash = Enumerable.Repeat((byte)0x20, 8).ToArray(), PassSalt = Enumerable.Repeat((byte)0x20, 8).ToArray() },

                        new AppUser { Username = "james1", FirstName = "james", LastName = "jones", Role = UserRole.Student, Email = "jj18g12@soton.ac.uk", PassHash = Enumerable.Repeat((byte)0x20, 8).ToArray(), PassSalt = Enumerable.Repeat((byte)0x20, 8).ToArray() },
                        new AppUser { Username = "lucy34", FirstName = "lucy", LastName = "smith", Role = UserRole.Student, Email = "ls1@solent.ac.uk", PassHash = Enumerable.Repeat((byte)0x20, 8).ToArray(), PassSalt = Enumerable.Repeat((byte)0x20, 8).ToArray() },
                        new AppUser { Username = "sarah-X", FirstName = "sarah", LastName = "xavier", Role = UserRole.Student, Email = "sx9g17@soton.ac.uk", PassHash = Enumerable.Repeat((byte)0x20, 8).ToArray(), PassSalt = Enumerable.Repeat((byte)0x20, 8).ToArray() },
                        new AppUser { Username = "alexthefantastic", FirstName = "alexander", LastName = "davies", Role = UserRole.Student, Email = "ad12g17@ecs.soton.ac.uk", PassHash = Enumerable.Repeat((byte)0x20, 8).ToArray(), PassSalt = Enumerable.Repeat((byte)0x20, 8).ToArray() },
                        new AppUser { Username = "jo92", FirstName = "jo", LastName = "strong", Role = UserRole.Student, Email = "js4@solent.ac.uk", PassHash = Enumerable.Repeat((byte)0x20, 8).ToArray(), PassSalt = Enumerable.Repeat((byte)0x20, 8).ToArray() }
                    );


            modelBuilder.Entity<Property>().HasData(
                        new Property { ID = 100, AppUserRef = "rb", AddressLine1 = "39 University Road", AddressLine2 = "", City = "Southampton", County = "Hampshire", Postcode = "SO146UP", PropertyDescription = "2 Toilets!", PropertyStatus = Models.Property.VerificationStatus.Rejected, Timestamp = DateTime.Parse("2018-01-01") },
                        new Property { ID = 101, AppUserRef = "bb", AddressLine1 = "30 Alma Road", AddressLine2 = "", City = "Southampton", County = "Hampshire", Postcode = "SO146UP", PropertyDescription = "Very nice brickwork", PropertyStatus = Models.Property.VerificationStatus.Approved, Timestamp = DateTime.Parse("2018-01-20") },
                        new Property { ID = 102, AppUserRef = "bb", AddressLine1 = "22 Portswood Road", AddressLine2 = "Portswood", City = "Southampton", County = "Hampshire", Postcode = "SO172ET", PropertyDescription = "It has a roof", PropertyStatus = Models.Property.VerificationStatus.Pending, Timestamp = DateTime.Parse("2018-01-29") },
                        new Property { ID = 103, AppUserRef = "bb", AddressLine1 = "31 Alma Road", AddressLine2 = "", City = "Southampton", County = "Hampshire", Postcode = "SO146UP", PropertyDescription = "Very very nice brickwork", PropertyStatus = Models.Property.VerificationStatus.Approved, Timestamp = DateTime.Parse("2018-01-31") }
                        );

            modelBuilder.Entity<Image>().HasData(
                        new Image { ID = 1, PropertyRef = 101, Position = 1, Path = "Alma/30-1" },
                        new Image { ID = 2, PropertyRef = 101, Position = 2, Path = "Alma/30-2" },
                        new Image { ID = 3, PropertyRef = 101, Position = 3, Path = "Alma/30-3" },

                        new Image { ID = 4, PropertyRef = 102, Position = 1, Path = "Portswood/22-1" },
                        new Image { ID = 5, PropertyRef = 102, Position = 2, Path = "Portswood/22-2" }
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
