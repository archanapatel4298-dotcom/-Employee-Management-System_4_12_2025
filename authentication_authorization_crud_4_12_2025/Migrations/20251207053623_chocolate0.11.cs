using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace authentication_authorization_crud_4_12_2025.Migrations
{
    /// <inheritdoc />
    public partial class chocolate011 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "user",
                table: "Chocolates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "user",
                table: "Chocolates");
        }
    }
}
