using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Housing.Migrations
{
    public partial class FifthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Property_AppUser_Username1",
                table: "Property");

            migrationBuilder.DropIndex(
                name: "IX_Property_Username1",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "Username1",
                table: "Property");

            migrationBuilder.RenameColumn(
                name: "UsernameRef",
                table: "Property",
                newName: "AppUserRef");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserRef",
                table: "Property",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Property_AppUserRef",
                table: "Property",
                column: "AppUserRef");

            migrationBuilder.AddForeignKey(
                name: "FK_Property_AppUser_AppUserRef",
                table: "Property",
                column: "AppUserRef",
                principalTable: "AppUser",
                principalColumn: "Username",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Property_AppUser_AppUserRef",
                table: "Property");

            migrationBuilder.DropIndex(
                name: "IX_Property_AppUserRef",
                table: "Property");

            migrationBuilder.RenameColumn(
                name: "AppUserRef",
                table: "Property",
                newName: "UsernameRef");

            migrationBuilder.AlterColumn<string>(
                name: "UsernameRef",
                table: "Property",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username1",
                table: "Property",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Property_Username1",
                table: "Property",
                column: "Username1");

            migrationBuilder.AddForeignKey(
                name: "FK_Property_AppUser_Username1",
                table: "Property",
                column: "Username1",
                principalTable: "AppUser",
                principalColumn: "Username",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
