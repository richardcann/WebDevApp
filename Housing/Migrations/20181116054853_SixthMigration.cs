using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Housing.Migrations
{
    public partial class SixthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Username",
                keyValue: "alexthefantastic",
                columns: new[] { "PassHash", "PassSalt" },
                values: new object[] { new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 }, new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 } });

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Username",
                keyValue: "bb",
                columns: new[] { "PassHash", "PassSalt" },
                values: new object[] { new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 }, new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 } });

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Username",
                keyValue: "james1",
                columns: new[] { "PassHash", "PassSalt" },
                values: new object[] { new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 }, new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 } });

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Username",
                keyValue: "jennaj",
                columns: new[] { "PassHash", "PassSalt" },
                values: new object[] { new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 }, new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 } });

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Username",
                keyValue: "jo92",
                columns: new[] { "PassHash", "PassSalt" },
                values: new object[] { new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 }, new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 } });

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Username",
                keyValue: "lucy34",
                columns: new[] { "PassHash", "PassSalt" },
                values: new object[] { new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 }, new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 } });

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Username",
                keyValue: "rb",
                columns: new[] { "PassHash", "PassSalt" },
                values: new object[] { new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 }, new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 } });

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Username",
                keyValue: "sarah-X",
                columns: new[] { "PassHash", "PassSalt" },
                values: new object[] { new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 }, new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 } });

            migrationBuilder.InsertData(
                table: "Property",
                columns: new[] { "ID", "AddressLine1", "AddressLine2", "AppUserRef", "City", "County", "Postcode", "PropertyDescription", "PropertyStatus", "Timestamp" },
                values: new object[] { 103, "31 Alma Road", "", "bb", "Southampton", "Hampshire", "SO146UP", "Very very nice brickwork", 0, new DateTime(2018, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Property",
                keyColumn: "ID",
                keyValue: 103);

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Username",
                keyValue: "alexthefantastic",
                columns: new[] { "PassHash", "PassSalt" },
                values: new object[] { new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 }, new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 } });

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Username",
                keyValue: "bb",
                columns: new[] { "PassHash", "PassSalt" },
                values: new object[] { new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 }, new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 } });

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Username",
                keyValue: "james1",
                columns: new[] { "PassHash", "PassSalt" },
                values: new object[] { new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 }, new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 } });

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Username",
                keyValue: "jennaj",
                columns: new[] { "PassHash", "PassSalt" },
                values: new object[] { new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 }, new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 } });

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Username",
                keyValue: "jo92",
                columns: new[] { "PassHash", "PassSalt" },
                values: new object[] { new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 }, new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 } });

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Username",
                keyValue: "lucy34",
                columns: new[] { "PassHash", "PassSalt" },
                values: new object[] { new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 }, new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 } });

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Username",
                keyValue: "rb",
                columns: new[] { "PassHash", "PassSalt" },
                values: new object[] { new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 }, new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 } });

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Username",
                keyValue: "sarah-X",
                columns: new[] { "PassHash", "PassSalt" },
                values: new object[] { new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 }, new byte[] { 32, 32, 32, 32, 32, 32, 32, 32 } });
        }
    }
}
