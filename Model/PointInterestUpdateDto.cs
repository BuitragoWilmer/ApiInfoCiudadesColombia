using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace InfoCity.API.Model
{
    /// <summary>
    /// DTO para actualizar un punto de interes
    /// </summary>
    public class PointInterestUpdateDto
    {
        /// <summary>
        /// Nombre del punto de interes
        /// </summary>
        [Required(ErrorMessage = "Debe ingresar el nombre del punto de interes")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Breve descripcion del punto de interes
        /// </summary>
        [AllowNull]
        [MaxLength(200)]
        public string Description { get; set; }
    }
}
