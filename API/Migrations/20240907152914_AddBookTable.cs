using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddBookTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CatId = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CatName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Category__6A1C8AFAB0F5DD60", x => x.CatId);
                });

            migrationBuilder.CreateTable(
                name: "sProduct",
                columns: table => new
                {
                    PId = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    PName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    QTY = table.Column<int>(type: "int", nullable: true),
                    CatId = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__sProduct__C5775540A21A559D", x => x.PId);
                    table.ForeignKey(
                        name: "FK_Category",
                        column: x => x.CatId,
                        principalTable: "Category",
                        principalColumn: "CatId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_sProduct_CatId",
                table: "sProduct",
                column: "CatId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sProduct");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
