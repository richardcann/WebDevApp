using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Housing.Migrations
{
    public partial class FourthMigration : Migration
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
