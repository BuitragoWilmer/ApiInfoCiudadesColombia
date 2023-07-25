using AutoMapper;

namespace InfoCity.API.Model
{
    public class CityWithoutPointInterestDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; }

    }
}
