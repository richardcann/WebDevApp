using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Housing.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Property_AppUser_LandlordUsername",
                table: "Property");

            migrationBuilder.RenameColumn(
                name: "LandlordUsername",
                table: "Property",
                newName: "Username1");

            migrationBuilder.RenameIndex(
                name: "IX_Property_LandlordUsername",
                table: "Property",
                newName: "IX_Property_Username1");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Property_AppUser_Username1",
                table: "Property",
                column: "Username1",
                principalTable: "AppUser",
                principalColumn: "Username",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Property_AppUser_Username1",
                table: "Property");

            migrationBuilder.RenameColumn(
                name: "Username1",
                table: "Property",
                newName: "LandlordUsername");

            migrationBuilder.RenameIndex(
                name: "IX_Property_Username1",
                table: "Property",
                newName: "IX_Property_LandlordUsername");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Property_AppUser_LandlordUsername",
                table: "Property",
                column: "LandlordUsername",
                principalTable: "AppUser",
                principalColumn: "Username",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
