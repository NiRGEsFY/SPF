using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPF.Migrations
{
    /// <inheritdoc />
    public partial class Ini_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LowDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    HighDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceLow = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImgListUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryGroups_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryGroups_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemsSpecification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Size = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Material = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    ProtectiveClass = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Character = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    MaterialInside = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Growth = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    MatingClass = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Thread = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Height = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsSpecification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemsSpecification_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemTypes_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemTypes_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TypeCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TypeCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TypeCategories_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryGroups_CategoryId",
                table: "CategoryGroups",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryGroups_GroupId",
                table: "CategoryGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsSpecification_ItemId",
                table: "ItemsSpecification",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemTypes_ItemId",
                table: "ItemTypes",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemTypes_TypeId",
                table: "ItemTypes",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeCategories_CategoryId",
                table: "TypeCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeCategories_TypeId",
                table: "TypeCategories",
                column: "TypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryGroups");

            migrationBuilder.DropTable(
                name: "ItemsSpecification");

            migrationBuilder.DropTable(
                name: "ItemTypes");

            migrationBuilder.DropTable(
                name: "TypeCategories");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Types");
        }
    }
}
