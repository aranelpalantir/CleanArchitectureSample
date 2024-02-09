using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchSample.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentityDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiry = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Name" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 2, 9, 13, 11, 59, 983, DateTimeKind.Unspecified).AddTicks(2765), new TimeSpan(0, 0, 0, 0, 0)), "Health, Tools & Shoes" });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "Name" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 2, 9, 13, 11, 59, 983, DateTimeKind.Unspecified).AddTicks(2779), new TimeSpan(0, 0, 0, 0, 0)), "Garden" });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "Name" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 2, 9, 13, 11, 59, 983, DateTimeKind.Unspecified).AddTicks(2801), new TimeSpan(0, 0, 0, 0, 0)), "Automotive, Beauty & Outdoors" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTimeOffset(new DateTime(2024, 2, 9, 13, 11, 59, 983, DateTimeKind.Unspecified).AddTicks(6578), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTimeOffset(new DateTime(2024, 2, 9, 13, 11, 59, 983, DateTimeKind.Unspecified).AddTicks(6582), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTimeOffset(new DateTime(2024, 2, 9, 13, 11, 59, 983, DateTimeKind.Unspecified).AddTicks(6584), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTimeOffset(new DateTime(2024, 2, 9, 13, 11, 59, 983, DateTimeKind.Unspecified).AddTicks(6587), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Description", "Title" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 2, 9, 13, 11, 59, 985, DateTimeKind.Unspecified).AddTicks(8927), new TimeSpan(0, 0, 0, 0, 0)), "Kapının olduğu eum dolore eve.", "Ex." });

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "Description", "Title" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 2, 9, 13, 11, 59, 985, DateTimeKind.Unspecified).AddTicks(8973), new TimeSpan(0, 0, 0, 0, 0)), "Ea incidunt sevindi ut quia.", "Orta sıradanlıktan." });

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "Description", "Title" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 2, 9, 13, 11, 59, 985, DateTimeKind.Unspecified).AddTicks(9041), new TimeSpan(0, 0, 0, 0, 0)), "Aliquam adanaya quis teldeki gazete.", "Aut autem suscipit." });

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "Description", "Title" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 2, 9, 13, 11, 59, 985, DateTimeKind.Unspecified).AddTicks(9080), new TimeSpan(0, 0, 0, 0, 0)), "Ötekinden gördüm neque doğru nesciunt.", "Aspernatur aliquam." });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Description", "Discount", "Price", "Title" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 2, 9, 13, 11, 59, 989, DateTimeKind.Unspecified).AddTicks(6597), new TimeSpan(0, 0, 0, 0, 0)), "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", 6.184823460661520m, 513.17m, "Awesome Frozen Gloves" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "Description", "Discount", "Price", "Title" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 2, 9, 13, 11, 59, 989, DateTimeKind.Unspecified).AddTicks(6642), new TimeSpan(0, 0, 0, 0, 0)), "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", 1.637687829775080m, 786.97m, "Refined Frozen Tuna" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "Description", "Discount", "Price", "Title" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 2, 9, 13, 11, 59, 989, DateTimeKind.Unspecified).AddTicks(6678), new TimeSpan(0, 0, 0, 0, 0)), "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", 9.691917034219520m, 52.92m, "Handcrafted Steel Salad" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Name" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 2, 8, 13, 14, 44, 101, DateTimeKind.Unspecified).AddTicks(5959), new TimeSpan(0, 0, 0, 0, 0)), "Computers, Tools & Home" });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "Name" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 2, 8, 13, 14, 44, 101, DateTimeKind.Unspecified).AddTicks(5982), new TimeSpan(0, 0, 0, 0, 0)), "Clothing, Garden & Movies" });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "Name" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 2, 8, 13, 14, 44, 101, DateTimeKind.Unspecified).AddTicks(5997), new TimeSpan(0, 0, 0, 0, 0)), "Movies, Computers & Automotive" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTimeOffset(new DateTime(2024, 2, 8, 13, 14, 44, 101, DateTimeKind.Unspecified).AddTicks(9873), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTimeOffset(new DateTime(2024, 2, 8, 13, 14, 44, 101, DateTimeKind.Unspecified).AddTicks(9876), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTimeOffset(new DateTime(2024, 2, 8, 13, 14, 44, 101, DateTimeKind.Unspecified).AddTicks(9879), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTimeOffset(new DateTime(2024, 2, 8, 13, 14, 44, 101, DateTimeKind.Unspecified).AddTicks(9881), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Description", "Title" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 2, 8, 13, 14, 44, 104, DateTimeKind.Unspecified).AddTicks(6646), new TimeSpan(0, 0, 0, 0, 0)), "Sevindi et veritatis et sayfası.", "Qui." });

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "Description", "Title" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 2, 8, 13, 14, 44, 104, DateTimeKind.Unspecified).AddTicks(6690), new TimeSpan(0, 0, 0, 0, 0)), "Ratione ışık quis tv totam.", "Dolor voluptas." });

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "Description", "Title" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 2, 8, 13, 14, 44, 104, DateTimeKind.Unspecified).AddTicks(6730), new TimeSpan(0, 0, 0, 0, 0)), "Molestiae salladı consequuntur quia consequatur.", "Vitae sandalye yapacakmış." });

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "Description", "Title" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 2, 8, 13, 14, 44, 104, DateTimeKind.Unspecified).AddTicks(6764), new TimeSpan(0, 0, 0, 0, 0)), "Dolores masaya veritatis teldeki çünkü.", "Bilgisayarı düşünüyor." });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Description", "Discount", "Price", "Title" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 2, 8, 13, 14, 44, 108, DateTimeKind.Unspecified).AddTicks(2245), new TimeSpan(0, 0, 0, 0, 0)), "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", 8.156987424701830m, 149.75m, "Handmade Steel Hat" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "Description", "Discount", "Price", "Title" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 2, 8, 13, 14, 44, 108, DateTimeKind.Unspecified).AddTicks(2279), new TimeSpan(0, 0, 0, 0, 0)), "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", 5.937172398364690m, 690.83m, "Sleek Plastic Pizza" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "Description", "Discount", "Price", "Title" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 2, 8, 13, 14, 44, 108, DateTimeKind.Unspecified).AddTicks(2379), new TimeSpan(0, 0, 0, 0, 0)), "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", 3.680152602010040m, 169.70m, "Small Plastic Ball" });
        }
    }
}
