using InfoCity.API.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfoCity.API.Entities
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CityId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }


        public ICollection<PointOfInterest> PointInterests { get; set; } =
            new List<PointOfInterest>();

        public City(string name) 
        {
            Name = name;
        }
    }
}
