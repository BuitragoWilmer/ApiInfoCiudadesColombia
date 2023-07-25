using System.Collections;
using System.Collections.Generic;

namespace InfoCity.API.Model
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }=string.Empty;
        public string Description { get; set; }

        public int numberPointsInterest {
            get {  return PointInterests.Count; }
        }

        public ICollection<PointInterestDto> PointInterests { get; set; }=
            new List<PointInterestDto>();
    }
}
