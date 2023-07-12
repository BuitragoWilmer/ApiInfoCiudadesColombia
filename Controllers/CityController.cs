using InfoCity.API.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace InfoCity.API.Controllers
{
    [ApiController]
    [Route("api/ciudad")]
    public class CityController: ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> GetCities()
        {
            return Ok(CitiesDataStore.current.Cities);
        }

        [HttpGet("{name}")]
        public ActionResult<CityDto> GetCity(string name)
        {
            CityDto city = CitiesDataStore.current.Cities.FirstOrDefault(x => x.Name == name);
            if(city == null)
            {
                return NotFound();
            }
            return Ok(city);
        }
    }
}
