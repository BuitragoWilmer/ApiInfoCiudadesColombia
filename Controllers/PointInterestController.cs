using InfoCity.API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace InfoCity.API.Controllers
{
    [Route("api/ciudad/{cityName}/puntosdeinteres")]
    [ApiController]
    public class PointInterestController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<PointInterestDto>> GetPointsInterest(string cityName)
        {
            var city = CitiesDataStore.current.Cities.FirstOrDefault(x => x.Name == cityName);
            if(city == null)
            {
                return NotFound();
            }
            return Ok(city.PointInterests);
        }

        [HttpGet("{pointInterest}")]
        public ActionResult<PointInterestDto> GetPointInterest(string cityName, int pointInterest) {
            var city = CitiesDataStore.current.Cities.FirstOrDefault(x => x.Name == cityName);
            if (city == null)
            {
                return NotFound();
            }
            var pointInterestReturn = city.PointInterests.FirstOrDefault(x=>x.Id == pointInterest);
            if (pointInterestReturn == null)
            {
                return NotFound();
            }
            return Ok(pointInterestReturn);
        }
    }
}
