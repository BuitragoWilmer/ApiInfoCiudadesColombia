using InfoCity.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace InfoCity.API.DbContexts
{
    public class CityInfoContext : DbContext
    {
        public DbSet<City> cities { get; set; }
        public DbSet<PointOfInterest> pointOfInterests { get; set;}

        public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(
                new City("Bogota")
                {
                    CityId = 1,
                    Description = "Bogota es la capital de Colombia."
                },
                new City("Medellin")
                {
                    CityId = 2,
                    Description = "Medellin es una ciudad vibrante y llena de cultura, rodeada de montañas y conocida por su clima agradable."
                },
                new City("Cali")
                {
                    CityId = 3,
                    Description = "Cali, conocida como la capital de la salsa, es una ciudad llena de ritmo, alegría y tradiciones colombianas."
                },
                new City("Barranquilla")
                {
                    CityId = 4,
                    Description = "Barranquilla es una ciudad costera con una mezcla única de culturas, famosa por su carnaval colorido y animado."
                },
                new City("Cartagena")
                {
                    CityId = 5,
                    Description = "Cartagena es una ciudad histórica con impresionantes fortificaciones y playas de aguas cristalinas."
                },
                new City("Santa Marta")
                {
                    CityId = 6,
                    Description = "Santa Marta es una joya costera con hermosos paisajes naturales, incluyendo la majestuosa Sierra Nevada de Santa Marta."
                },
                new City("Manizales")
                {
                    CityId = 7,
                    Description = "Manizales es una ciudad ubicada en la zona cafetera de Colombia, rodeada de verdes montañas y con un clima agradable."
                },
                new City("Pereira")
                {
                    CityId = 8,
                    Description = "Pereira es una ciudad dinámica y moderna, conocida por ser parte del Eje Cafetero y sus extensos paisajes verdes."
                },
                new City("Pasto")
                {
                    CityId = 9,
                    Description = "Pasto es una ciudad andina con una rica herencia cultural y tradiciones ancestrales arraigadas en su identidad."
                },
                new City("Ibagué")
                {
                    CityId = 10,
                    Description = "Ibagué es conocida como la 'Ciudad Musical de Colombia' y es famosa por su folclore y festivales musicales."
                },
                new City("Neiva")
                {
                    CityId = 11,
                    Description = "Neiva es una ciudad cálida y acogedora, ubicada en la región del Huila, con bellos paisajes y cultura cafetera."
                });
            modelBuilder.Entity<PointOfInterest>().HasData(
                new PointOfInterest("Monserrate")
                {
                    PointInterestId = 1,
                    CityId = 1,
                    Description = "Monserrate ofrece una vista panorámica impresionante de Bogotá y es un lugar sagrado de peregrinación."
                },
                new PointOfInterest("Parque Arví")
                {
                    PointInterestId = 2,
                    CityId = 2,
                    Description = "El Parque Arví es un oasis natural cerca de Medellín, ideal para caminatas, ecoturismo y experiencias al aire libre."
                },
                new PointOfInterest("Cristo Rey")
                {
                    PointInterestId = 3,
                    CityId = 3,
                    Description = "El Cristo Rey es una majestuosa estatua ubicada en una colina, que ofrece una vista panorámica de la ciudad de Cali."
                },
                new PointOfInterest("Casa del Carnaval")
                {
                    PointInterestId = 4,
                    CityId = 4,
                    Description = "La Casa del Carnaval es un lugar interactivo que muestra la historia y la tradición del carnaval barranquillero."
                },
                new PointOfInterest("Ciudad Amurallada")
                {
                    PointInterestId = 5,
                    CityId = 5,
                    Description = "La Ciudad Amurallada de Cartagena es un impresionante sitio histórico con calles adoquinadas y encanto colonial."
                },
                new PointOfInterest("Parque Nacional Natural Tayrona")
                {
                    PointInterestId = 6,
                    CityId = 6,
                    Description = "El Parque Nacional Natural Tayrona es un paraíso natural con playas de arena blanca y exuberante vegetación."
                },
                new PointOfInterest("Catedral de Manizales")
                {
                    PointInterestId = 7,
                    CityId = 7,
                    Description = "La majestuosa Catedral de Manizales se eleva sobre la ciudad y ofrece una vista panorámica de los paisajes circundantes."
                },
                new PointOfInterest("Termales de Santa Rosa de Cabal")
                {
                    PointInterestId = 8,
                    CityId = 8,
                    Description = "Los Termales de Santa Rosa de Cabal son aguas termales naturales en medio de la naturaleza, perfectas para relajarse."
                },
                new PointOfInterest("Laguna de la Cocha")
                {
                    PointInterestId = 9,
                    CityId = 9,
                    Description = "La Laguna de la Cocha es un hermoso lago rodeado de montañas y un destino popular para turistas y lugareños."
                },
                new PointOfInterest("Jardín Botánico San Jorge")
                {
                    PointInterestId = 10,
                    CityId = 10,
                    Description = "El Jardín Botánico San Jorge ofrece una colección impresionante de flora y es un oasis de tranquilidad en Ibagué."
                },
                new PointOfInterest("Desierto de la Tatacoa")
                {
                    PointInterestId = 11,
                    CityId = 11,
                    Description = "El Desierto de la Tatacoa es una formación de paisaje único con cañones y áreas desérticas, perfecto para la observación de estrellas."
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}
