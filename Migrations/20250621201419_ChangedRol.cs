using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConversorMonedas.Migrations
{
    /// <inheritdoc />
    public partial class ChangedRol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Users",
                newName: "Rol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rol",
                table: "Users",
                newName: "Role");
        }
    }
}
