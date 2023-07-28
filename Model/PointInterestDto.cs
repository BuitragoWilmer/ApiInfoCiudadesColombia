using System.Diagnostics.CodeAnalysis;

namespace InfoCity.API.Model
{
    public class PointInterestDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        [AllowNull]
        public string Description { get; set; }
    }
}
