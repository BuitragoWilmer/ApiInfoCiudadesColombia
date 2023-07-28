using InfoCity.API.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace InfoCity.API.Entities
{
#pragma warning disable CS1591
    
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CityId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        [AllowNull]
        public string Description { get; set; }


        public ICollection<PointOfInterest> PointInterests { get; set; } =
            new List<PointOfInterest>();

        public City(string name) 
        {
            Name = name;
        }
    }

#pragma warning restore CS1591
}
