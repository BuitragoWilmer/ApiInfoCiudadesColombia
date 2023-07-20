using InfoCity.API.Model;
using System.Collections.Generic;

namespace InfoCity.API
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }

        public CitiesDataStore() {
            Cities = new List<CityDto>() {
                new CityDto()
                {
                    Id = 1,
                    Name = "Bogota",
                    Description = "Altura",
                    PointInterests = new List<PointInterestDto>()
                    {
                        new PointInterestDto()
                        {
                            Id=1,
                            Name="Plaza de Bolivar",
                            Description="Muchas Aves"
                        },
                        new PointInterestDto()
                        {
                            Id = 1,
                            Name = "Monserrate",
                            Description = "Es un cerro con una iglesia"
                        }
                     }
                },
                new CityDto()
                {
                    Id = 2,
                    Name = "Cali",
                    Description = "salsa",
                    PointInterests = new List<PointInterestDto>()
                    {
                        new PointInterestDto()
                        {
                            Id=1,
                            Name="Cristo",
                            Description="Cerro con Cristo"
                        },
                        new PointInterestDto()
                        {
                            Id = 1,
                            Name = "Ermita",
                            Description = "Es una iglesia"
                        }
                     }
                },
                new CityDto()
                {
                    Id = 3,
                    Name = "Cartagena",
                    Description = "Muralla",
                    PointInterests = new List<PointInterestDto>()
                    {
                        new PointInterestDto()
                        {
                            Id=1,
                            Name="La ciudad amurallada",
                            Description="Es el centro histórico de la ciudad de Cartagena de Indias y ha sido declarado tanto patrimonio nacional de Colombia como Patrimonio de la Humanidad. Aquí vivían los nobles y las personas importantes en la época de la Colonia"
                        },                      
                     }
                }
            };
        }
    }
}
