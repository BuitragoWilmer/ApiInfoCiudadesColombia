using System.ComponentModel.DataAnnotations;

namespace InfoCity.API.Model
{
    public class PointInterestCreationDto
    {
        [Required(ErrorMessage = "Debe ingresar el nombre del punto de interes")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
