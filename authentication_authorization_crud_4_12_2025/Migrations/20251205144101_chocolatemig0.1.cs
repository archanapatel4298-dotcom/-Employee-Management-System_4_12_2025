using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace authentication_authorization_crud_4_12_2025.Migrations
{
    /// <inheritdoc />
    public partial class chocolatemig01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chocolates",
                columns: table => new
                {
                    ChocolateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    choco_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    choco_company = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    choco_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cost = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chocolates", x => x.ChocolateId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chocolates");
        }
    }
}
