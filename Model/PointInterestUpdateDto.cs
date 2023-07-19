﻿using System.ComponentModel.DataAnnotations;

namespace InfoCity.API.Model
{
    public class PointInterestUpdateDto
    {
        [Required(ErrorMessage = "Debe ingresar el nombre del punto de interes")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Description { get; set; }
    }
}