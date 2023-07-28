using AutoMapper;

namespace InfoCity.API.Model
{
    /// <summary>
    /// DTO para una ciudad que no requiera puntos de interes
    /// </summary>
    public class CityWithoutPointInterestDto
    {
        /// <summary>
        /// El id de la ciudad
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre de la ciudad
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descripcion de la ciudad
        /// </summary>
        public string Description { get; set; }

    }
}
