using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Housing.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppUser",
                columns: new[] { "Username", "Email", "FirstName", "LastName", "PassHash", "PassSalt", "Role" },
                values: new object[,]
                {
                    { "bb", "bob@barker.com", "bob", "barker", new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 }, new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 }, 1 },
                    { "rb", "rob@barker.com", "rob", "barker", new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 }, new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 }, 1 },
                    { "jennaj", "jenna@accomodationservices.soton.ac.uk", "jenna", "jenkins", new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 }, new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 }, 0 },
                    { "james1", "jj18g12@soton.ac.uk", "james", "jones", new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 }, new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 }, 2 },
                    { "lucy34", "ls1@solent.ac.uk", "lucy", "smith", new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 }, new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 }, 2 },
                    { "sarah-X", "sx9g17@soton.ac.uk", "sarah", "xavier", new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 }, new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 }, 2 },
                    { "alexthefantastic", "ad12g17@ecs.soton.ac.uk", "alexander", "davies", new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 }, new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 }, 2 },
                    { "jo92", "js4@solent.ac.uk", "jo", "strong", new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 }, new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 }, 2 }
                });

            migrationBuilder.InsertData(
                table: "Property",
                columns: new[] { "ID", "AddressLine1", "AddressLine2", "City", "County", "LandlordUsername", "Postcode", "PropertyDescription", "PropertyStatus", "Timestamp", "UsernameRef" },
                values: new object[,]
                {
                    { 100, "39 University Road", "", "Southampton", "Hampshire", null, "SO146UP", "2 Toilets!", 2, new DateTime(2018, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "rb" },
                    { 101, "30 Alma Road", "", "Southampton", "Hampshire", null, "SO146UP", "Very nice brickwork", 0, new DateTime(2018, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "bb" },
                    { 102, "22 Portswood Road", "Portswood", "Southampton", "Hampshire", null, "SO172ET", "It has a roof", 1, new DateTime(2018, 1, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "bb" }
                });

            migrationBuilder.InsertData(
                table: "Image",
                columns: new[] { "ID", "Path", "Position", "PropertyRef" },
                values: new object[,]
                {
                    { 1, "Alma/30-1", 1, 101 },
                    { 2, "Alma/30-2", 2, 101 },
                    { 3, "Alma/30-3", 3, 101 },
                    { 4, "Portswood/22-1", 1, 102 },
                    { 5, "Portswood/22-2", 2, 102 }
                });

            migrationBuilder.InsertData(
                table: "Rejection",
                columns: new[] { "ID", "Description", "PropertyRef", "Timestamp" },
                values: new object[] { 1, "Please add at least one photo", 100, new DateTime(2018, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUser",
                keyColumn: "Username",
                keyValue: "alexthefantastic");

            migrationBuilder.DeleteData(
                table: "AppUser",
                keyColumn: "Username",
                keyValue: "bb");

            migrationBuilder.DeleteData(
                table: "AppUser",
                keyColumn: "Username",
                keyValue: "james1");

            migrationBuilder.DeleteData(
                table: "AppUser",
                keyColumn: "Username",
                keyValue: "jennaj");

            migrationBuilder.DeleteData(
                table: "AppUser",
                keyColumn: "Username",
                keyValue: "jo92");

            migrationBuilder.DeleteData(
                table: "AppUser",
                keyColumn: "Username",
                keyValue: "lucy34");

            migrationBuilder.DeleteData(
                table: "AppUser",
                keyColumn: "Username",
                keyValue: "rb");

            migrationBuilder.DeleteData(
                table: "AppUser",
                keyColumn: "Username",
                keyValue: "sarah-X");

            migrationBuilder.DeleteData(
                table: "Image",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Image",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Image",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Image",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Image",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Rejection",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Property",
                keyColumn: "ID",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "Property",
                keyColumn: "ID",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Property",
                keyColumn: "ID",
                keyValue: 102);
        }
    }
}
