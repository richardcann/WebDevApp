using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Housing.Migrations
{
    public partial class CreateHousingDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUser",
                columns: table => new
                {
                    Username = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Role = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    PassHash = table.Column<byte[]>(nullable: true),
                    PassSalt = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUser", x => x.Username);
                });

            migrationBuilder.CreateTable(
                name: "Property",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressLine1 = table.Column<string>(nullable: true),
                    AddressLine2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    County = table.Column<string>(nullable: true),
                    Postcode = table.Column<string>(nullable: true),
                    PropertyDescription = table.Column<string>(nullable: true),
                    PropertyStatus = table.Column<int>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    UsernameRef = table.Column<string>(nullable: true),
                    LandlordUsername = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Property", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Property_AppUser_LandlordUsername",
                        column: x => x.LandlordUsername,
                        principalTable: "AppUser",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Position = table.Column<int>(nullable: false),
                    Path = table.Column<string>(nullable: true),
                    PropertyRef = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Image_Property_PropertyRef",
                        column: x => x.PropertyRef,
                        principalTable: "Property",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rejection",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    PropertyRef = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rejection", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Rejection_Property_PropertyRef",
                        column: x => x.PropertyRef,
                        principalTable: "Property",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Image_PropertyRef",
                table: "Image",
                column: "PropertyRef");

            migrationBuilder.CreateIndex(
                name: "IX_Property_LandlordUsername",
                table: "Property",
                column: "LandlordUsername");

            migrationBuilder.CreateIndex(
                name: "IX_Rejection_PropertyRef",
                table: "Rejection",
                column: "PropertyRef");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "Rejection");

            migrationBuilder.DropTable(
                name: "Property");

            migrationBuilder.DropTable(
                name: "AppUser");
        }
    }
}
