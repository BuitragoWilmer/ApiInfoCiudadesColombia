using Microsoft.EntityFrameworkCore.Migrations;

namespace InfoCity.API.Migrations
{
    public partial class CityInfoDBInitialAddMunicipio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Municipio",
                table: "cities",
                type: "TEXT",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Municipio",
                table: "cities");
        }
    }
}
