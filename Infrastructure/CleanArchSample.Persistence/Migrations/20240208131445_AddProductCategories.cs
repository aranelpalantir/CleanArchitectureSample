using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchSample.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddProductCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryProduct");

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => new { x.ProductId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_ProductCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCategories_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_CategoryId",
                table: "ProductCategories",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.CreateTable(
                name: "CategoryProduct",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryProduct", x => new { x.CategoriesId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_CategoryProduct_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Name" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 2, 7, 13, 13, 44, 421, DateTimeKind.Unspecified).AddTicks(4010), new TimeSpan(0, 0, 0, 0, 0)), "Books, Books & Grocery" });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "Name" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 2, 7, 13, 13, 44, 421, DateTimeKind.Unspecified).AddTicks(4023), new TimeSpan(0, 0, 0, 0, 0)), "Toys & Automotive" });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "Name" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 2, 7, 13, 13, 44, 421, DateTimeKind.Unspecified).AddTicks(4037), new TimeSpan(0, 0, 0, 0, 0)), "Music, Baby & Sports" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTimeOffset(new DateTime(2024, 2, 7, 13, 13, 44, 421, DateTimeKind.Unspecified).AddTicks(6722), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTimeOffset(new DateTime(2024, 2, 7, 13, 13, 44, 421, DateTimeKind.Unspecified).AddTicks(6724), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTimeOffset(new DateTime(2024, 2, 7, 13, 13, 44, 421, DateTimeKind.Unspecified).AddTicks(6725), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTimeOffset(new DateTime(2024, 2, 7, 13, 13, 44, 421, DateTimeKind.Unspecified).AddTicks(6726), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Description", "Title" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 2, 7, 13, 13, 44, 423, DateTimeKind.Unspecified).AddTicks(3626), new TimeSpan(0, 0, 0, 0, 0)), "Eve dergi sıradanlıktan sandalye kulu.", "Makinesi." });

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "Description", "Title" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 2, 7, 13, 13, 44, 423, DateTimeKind.Unspecified).AddTicks(3653), new TimeSpan(0, 0, 0, 0, 0)), "Duyulmamış anlamsız yaptı sokaklarda eaque.", "Quis praesentium." });

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "Description", "Title" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 2, 7, 13, 13, 44, 423, DateTimeKind.Unspecified).AddTicks(3721), new TimeSpan(0, 0, 0, 0, 0)), "Oldular ea dicta yapacakmış değerli.", "Cesurca aliquid aut." });

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "Description", "Title" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 2, 7, 13, 13, 44, 423, DateTimeKind.Unspecified).AddTicks(3745), new TimeSpan(0, 0, 0, 0, 0)), "Sed patlıcan modi ea yapacakmış.", "Otobüs magni." });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Description", "Discount", "Price", "Title" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 2, 7, 13, 13, 44, 426, DateTimeKind.Unspecified).AddTicks(2292), new TimeSpan(0, 0, 0, 0, 0)), "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", 2.030225400947830m, 621.65m, "Incredible Concrete Shirt" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "Description", "Discount", "Price", "Title" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 2, 7, 13, 13, 44, 426, DateTimeKind.Unspecified).AddTicks(2315), new TimeSpan(0, 0, 0, 0, 0)), "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", 4.009944620906370m, 899.05m, "Incredible Cotton Keyboard" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "Description", "Discount", "Price", "Title" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 2, 7, 13, 13, 44, 426, DateTimeKind.Unspecified).AddTicks(2333), new TimeSpan(0, 0, 0, 0, 0)), "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", 5.220357196207630m, 551.82m, "Ergonomic Cotton Pants" });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryProduct_ProductsId",
                table: "CategoryProduct",
                column: "ProductsId");
        }
    }
}
