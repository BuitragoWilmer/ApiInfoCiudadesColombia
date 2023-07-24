﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfoCity.API.Entities
{
    public class PointOfInterest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PointInterestId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public string? Description { get; set; }

        [ForeignKey("CityId")]
        public City? City { get; set; }

        public int CityId { get; set; }

        public PointOfInterest(string name) 
        {
        Name = name;
        }
    }
}