using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Housing.Migrations
{
    public partial class Release1 : Migration
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
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    PropertyStatus = table.Column<int>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    AppUserRef = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Property", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Property_AppUser_AppUserRef",
                        column: x => x.AppUserRef,
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

            migrationBuilder.InsertData(
                table: "AppUser",
                columns: new[] { "Username", "Email", "FirstName", "LastName", "PassHash", "PassSalt", "Role" },
                values: new object[,]
                {
                    { "bb", "bob@barker.com", "bob", "barker", new byte[] { 86, 52, 101, 110, 84, 99, 83, 102, 114, 68, 78, 114, 110, 74, 65, 52, 74, 73, 66, 109, 90, 69, 89, 90, 82, 99, 99, 67, 105, 88, 120, 83, 71, 104, 97, 80, 78, 90, 56, 109, 75, 101, 103, 61 }, new byte[] { 170, 226, 88, 55, 3, 10, 240, 92, 41, 41, 120, 243, 49, 222, 110, 181 }, 1 },
                    { "rb", "rob@barker.com", "rob", "barker", new byte[] { 100, 49, 87, 52, 73, 75, 117, 102, 114, 87, 48, 51, 80, 113, 66, 85, 43, 81, 115, 56, 73, 116, 107, 113, 85, 53, 108, 99, 82, 99, 106, 84, 66, 120, 52, 117, 121, 83, 68, 78, 90, 52, 89, 61 }, new byte[] { 220, 11, 92, 219, 13, 121, 119, 37, 204, 189, 166, 18, 64, 216, 146, 0 }, 1 },
                    { "jennaj", "jenna@accomodationservices.soton.ac.uk", "jenna", "jenkins", new byte[] { 73, 116, 53, 112, 57, 119, 98, 86, 98, 54, 65, 102, 73, 90, 115, 107, 100, 110, 53, 98, 87, 101, 97, 66, 105, 97, 120, 57, 71, 75, 114, 83, 106, 49, 53, 115, 119, 48, 102, 66, 48, 83, 77, 61 }, new byte[] { 83, 62, 209, 222, 65, 42, 68, 22, 67, 179, 195, 6, 86, 173, 17, 83 }, 0 },
                    { "james1", "jj18g12@soton.ac.uk", "james", "jones", new byte[] { 118, 73, 98, 118, 80, 112, 107, 111, 112, 73, 89, 98, 52, 121, 97, 110, 71, 114, 76, 55, 86, 114, 69, 70, 50, 84, 49, 104, 101, 117, 118, 73, 121, 56, 120, 47, 90, 65, 100, 84, 105, 76, 111, 61 }, new byte[] { 87, 249, 212, 34, 178, 242, 73, 89, 190, 134, 198, 45, 104, 247, 147, 172 }, 2 },
                    { "lucy34", "ls1@solent.ac.uk", "lucy", "smith", new byte[] { 117, 88, 75, 84, 66, 90, 108, 69, 71, 66, 85, 86, 74, 86, 111, 107, 104, 111, 68, 81, 81, 73, 72, 85, 55, 99, 109, 102, 68, 83, 79, 99, 119, 112, 49, 56, 83, 66, 87, 65, 106, 73, 119, 61 }, new byte[] { 78, 58, 118, 152, 181, 95, 9, 193, 58, 232, 249, 10, 221, 233, 18, 205 }, 2 },
                    { "sarah-X", "sx9g17@soton.ac.uk", "sarah", "xavier", new byte[] { 117, 80, 121, 117, 108, 55, 85, 103, 84, 99, 98, 68, 67, 55, 106, 106, 98, 83, 105, 86, 73, 117, 113, 70, 50, 90, 84, 97, 111, 67, 113, 111, 74, 108, 84, 84, 75, 105, 55, 57, 76, 79, 89, 61 }, new byte[] { 3, 41, 221, 8, 163, 31, 47, 119, 131, 156, 1, 36, 73, 117, 21, 179 }, 2 },
                    { "alexthefantastic", "ad12g17@ecs.soton.ac.uk", "alexander", "davies", new byte[] { 103, 113, 97, 74, 116, 69, 65, 117, 110, 88, 75, 109, 117, 88, 66, 71, 80, 104, 81, 122, 43, 73, 110, 67, 51, 121, 102, 69, 112, 55, 87, 88, 99, 74, 113, 105, 48, 67, 85, 98, 65, 101, 85, 61 }, new byte[] { 124, 12, 164, 118, 20, 110, 205, 123, 64, 63, 142, 56, 140, 40, 208, 181 }, 2 },
                    { "jo92", "js4@solent.ac.uk", "jo", "strong", new byte[] { 85, 72, 65, 50, 110, 118, 100, 84, 69, 90, 117, 83, 107, 55, 111, 68, 76, 119, 109, 113, 76, 71, 55, 75, 84, 49, 97, 100, 120, 105, 109, 43, 118, 53, 97, 106, 97, 72, 105, 116, 114, 114, 69, 61 }, new byte[] { 33, 190, 184, 105, 243, 213, 124, 181, 10, 5, 221, 74, 56, 130, 26, 203 }, 2 }
                });

            migrationBuilder.InsertData(
                table: "Property",
                columns: new[] { "ID", "AddressLine1", "AddressLine2", "AppUserRef", "City", "County", "Latitude", "Longitude", "Postcode", "PropertyDescription", "PropertyStatus", "Timestamp" },
                values: new object[,]
                {
                    { 101, "30 Alma Road", "", "bb", "Southampton", "Hampshire", 50.922176499999999, -1.3997237, "SO146UP", "Very nice brickwork", 0, new DateTime(2018, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 102, "22 Portswood Road", "Portswood", "bb", "Southampton", "Hampshire", 50.921070100000001, -1.3976039, "SO172ET", "It has a roof", 1, new DateTime(2018, 1, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 103, "31 Alma Road", "", "bb", "Southampton", "Hampshire", 50.921907400000002, -1.4000154, "SO146UP", "Very very nice brickwork", 0, new DateTime(2018, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 100, "39 University Road", "", "rb", "Southampton", "Hampshire", 50.933448599999998, -1.3978988999999999, "SO146UP", "2 Toilets!", 2, new DateTime(2018, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Image",
                columns: new[] { "ID", "Path", "Position", "PropertyRef" },
                values: new object[,]
                {
                    { 1, "1aa7f469-72fb-4f9b-9861-cd5445dd3486.jpeg", 1, 101 },
                    { 2, "2a757997-283f-4e32-95af-71460a81c87f.jpeg", 2, 101 },
                    { 3, "2db635bf-6d9f-45db-8da9-837f2cea0db8.jpeg", 3, 101 },
                    { 4, "1ea369fe-8059-44f6-8b48-b17211d8d26b.jpeg", 1, 102 },
                    { 5, "3aa72703-a50a-40c0-9871-a85cb78837c8.jpeg", 2, 102 }
                });

            migrationBuilder.InsertData(
                table: "Rejection",
                columns: new[] { "ID", "Description", "PropertyRef", "Timestamp" },
                values: new object[] { 1, "Please add at least one photo", 100, new DateTime(2018, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_Image_PropertyRef",
                table: "Image",
                column: "PropertyRef");

            migrationBuilder.CreateIndex(
                name: "IX_Property_AppUserRef",
                table: "Property",
                column: "AppUserRef");

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
