using Microsoft.EntityFrameworkCore.Migrations;

namespace InfoCity.API.Migrations
{
    public partial class DataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Municipio",
                table: "cities");

            migrationBuilder.InsertData(
                table: "cities",
                columns: new[] { "CityId", "Description", "Name" },
                values: new object[] { 1, "Bogota es la capital de Colombia.", "Bogota" });

            migrationBuilder.InsertData(
                table: "cities",
                columns: new[] { "CityId", "Description", "Name" },
                values: new object[] { 2, "Medellin es una ciudad vibrante y llena de cultura, rodeada de montañas y conocida por su clima agradable.", "Medellin" });

            migrationBuilder.InsertData(
                table: "cities",
                columns: new[] { "CityId", "Description", "Name" },
                values: new object[] { 3, "Cali, conocida como la capital de la salsa, es una ciudad llena de ritmo, alegría y tradiciones colombianas.", "Cali" });

            migrationBuilder.InsertData(
                table: "cities",
                columns: new[] { "CityId", "Description", "Name" },
                values: new object[] { 4, "Barranquilla es una ciudad costera con una mezcla única de culturas, famosa por su carnaval colorido y animado.", "Barranquilla" });

            migrationBuilder.InsertData(
                table: "cities",
                columns: new[] { "CityId", "Description", "Name" },
                values: new object[] { 5, "Cartagena es una ciudad histórica con impresionantes fortificaciones y playas de aguas cristalinas.", "Cartagena" });

            migrationBuilder.InsertData(
                table: "cities",
                columns: new[] { "CityId", "Description", "Name" },
                values: new object[] { 6, "Santa Marta es una joya costera con hermosos paisajes naturales, incluyendo la majestuosa Sierra Nevada de Santa Marta.", "Santa Marta" });

            migrationBuilder.InsertData(
                table: "cities",
                columns: new[] { "CityId", "Description", "Name" },
                values: new object[] { 7, "Manizales es una ciudad ubicada en la zona cafetera de Colombia, rodeada de verdes montañas y con un clima agradable.", "Manizales" });

            migrationBuilder.InsertData(
                table: "cities",
                columns: new[] { "CityId", "Description", "Name" },
                values: new object[] { 8, "Pereira es una ciudad dinámica y moderna, conocida por ser parte del Eje Cafetero y sus extensos paisajes verdes.", "Pereira" });

            migrationBuilder.InsertData(
                table: "cities",
                columns: new[] { "CityId", "Description", "Name" },
                values: new object[] { 9, "Pasto es una ciudad andina con una rica herencia cultural y tradiciones ancestrales arraigadas en su identidad.", "Pasto" });

            migrationBuilder.InsertData(
                table: "cities",
                columns: new[] { "CityId", "Description", "Name" },
                values: new object[] { 10, "Ibagué es conocida como la 'Ciudad Musical de Colombia' y es famosa por su folclore y festivales musicales.", "Ibagué" });

            migrationBuilder.InsertData(
                table: "cities",
                columns: new[] { "CityId", "Description", "Name" },
                values: new object[] { 11, "Neiva es una ciudad cálida y acogedora, ubicada en la región del Huila, con bellos paisajes y cultura cafetera.", "Neiva" });

            migrationBuilder.InsertData(
                table: "pointOfInterests",
                columns: new[] { "PointInterestId", "CityId", "Description", "Name" },
                values: new object[] { 1, 1, "Monserrate ofrece una vista panorámica impresionante de Bogotá y es un lugar sagrado de peregrinación.", "Monserrate" });

            migrationBuilder.InsertData(
                table: "pointOfInterests",
                columns: new[] { "PointInterestId", "CityId", "Description", "Name" },
                values: new object[] { 2, 2, "El Parque Arví es un oasis natural cerca de Medellín, ideal para caminatas, ecoturismo y experiencias al aire libre.", "Parque Arví" });

            migrationBuilder.InsertData(
                table: "pointOfInterests",
                columns: new[] { "PointInterestId", "CityId", "Description", "Name" },
                values: new object[] { 3, 3, "El Cristo Rey es una majestuosa estatua ubicada en una colina, que ofrece una vista panorámica de la ciudad de Cali.", "Cristo Rey" });

            migrationBuilder.InsertData(
                table: "pointOfInterests",
                columns: new[] { "PointInterestId", "CityId", "Description", "Name" },
                values: new object[] { 4, 4, "La Casa del Carnaval es un lugar interactivo que muestra la historia y la tradición del carnaval barranquillero.", "Casa del Carnaval" });

            migrationBuilder.InsertData(
                table: "pointOfInterests",
                columns: new[] { "PointInterestId", "CityId", "Description", "Name" },
                values: new object[] { 5, 5, "La Ciudad Amurallada de Cartagena es un impresionante sitio histórico con calles adoquinadas y encanto colonial.", "Ciudad Amurallada" });

            migrationBuilder.InsertData(
                table: "pointOfInterests",
                columns: new[] { "PointInterestId", "CityId", "Description", "Name" },
                values: new object[] { 6, 6, "El Parque Nacional Natural Tayrona es un paraíso natural con playas de arena blanca y exuberante vegetación.", "Parque Nacional Natural Tayrona" });

            migrationBuilder.InsertData(
                table: "pointOfInterests",
                columns: new[] { "PointInterestId", "CityId", "Description", "Name" },
                values: new object[] { 7, 7, "La majestuosa Catedral de Manizales se eleva sobre la ciudad y ofrece una vista panorámica de los paisajes circundantes.", "Catedral de Manizales" });

            migrationBuilder.InsertData(
                table: "pointOfInterests",
                columns: new[] { "PointInterestId", "CityId", "Description", "Name" },
                values: new object[] { 8, 8, "Los Termales de Santa Rosa de Cabal son aguas termales naturales en medio de la naturaleza, perfectas para relajarse.", "Termales de Santa Rosa de Cabal" });

            migrationBuilder.InsertData(
                table: "pointOfInterests",
                columns: new[] { "PointInterestId", "CityId", "Description", "Name" },
                values: new object[] { 9, 9, "La Laguna de la Cocha es un hermoso lago rodeado de montañas y un destino popular para turistas y lugareños.", "Laguna de la Cocha" });

            migrationBuilder.InsertData(
                table: "pointOfInterests",
                columns: new[] { "PointInterestId", "CityId", "Description", "Name" },
                values: new object[] { 10, 10, "El Jardín Botánico San Jorge ofrece una colección impresionante de flora y es un oasis de tranquilidad en Ibagué.", "Jardín Botánico San Jorge" });

            migrationBuilder.InsertData(
                table: "pointOfInterests",
                columns: new[] { "PointInterestId", "CityId", "Description", "Name" },
                values: new object[] { 11, 11, "El Desierto de la Tatacoa es una formación de paisaje único con cañones y áreas desérticas, perfecto para la observación de estrellas.", "Desierto de la Tatacoa" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "pointOfInterests",
                keyColumn: "PointInterestId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "pointOfInterests",
                keyColumn: "PointInterestId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "pointOfInterests",
                keyColumn: "PointInterestId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "pointOfInterests",
                keyColumn: "PointInterestId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "pointOfInterests",
                keyColumn: "PointInterestId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "pointOfInterests",
                keyColumn: "PointInterestId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "pointOfInterests",
                keyColumn: "PointInterestId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "pointOfInterests",
                keyColumn: "PointInterestId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "pointOfInterests",
                keyColumn: "PointInterestId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "pointOfInterests",
                keyColumn: "PointInterestId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "pointOfInterests",
                keyColumn: "PointInterestId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "CityId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "CityId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "CityId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "CityId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "CityId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "CityId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "CityId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "CityId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "CityId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "CityId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "CityId",
                keyValue: 11);

            migrationBuilder.AddColumn<string>(
                name: "Municipio",
                table: "cities",
                type: "TEXT",
                maxLength: 50,
                nullable: true);
        }
    }
}
