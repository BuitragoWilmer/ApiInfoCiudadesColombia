using InfoCity.API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace InfoCity.API.OperationFilters
{
    public class GetCityOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.OperationId != "GetCities")
            {
                return;
            }
            if (operation.Responses.Any(r => r.Key == StatusCodes.Status200OK.ToString()))
            {
                var schema = context.SchemaGenerator.GenerateSchema(
                    typeof(CityDto), context.SchemaRepository
                    );
                operation.Responses[StatusCodes.Status200OK.ToString()].Content.Add(
                        "application/vnd.marvin.CityWithoutPointInterestDto+json", new OpenApiMediaType() { Schema = schema }
                    );
            }             
        }
    }
}
