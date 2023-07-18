using InfoCity.API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
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

        [HttpGet("{pointInterest}", Name = "GetPointInterest")]
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

        [HttpPost]
        public ActionResult<PointInterestDto> CreatePointInterest(
            string cityName,
            PointInterestCreationDto pointInterest)
        {
            var city = CitiesDataStore.current.Cities.FirstOrDefault(x => x.Name == cityName);
            if (city == null)
            {
                return NotFound();
            }
            var maxPointInterestId = CitiesDataStore.current.Cities.SelectMany(x=>x.PointInterests).Max(x=>x.Id);
            var finalPointInterest = new PointInterestDto()
            {
                Id = ++maxPointInterestId,
                Name = pointInterest.Name,
                Description = pointInterest.Description
            };
            city.PointInterests.Add(finalPointInterest);
            return CreatedAtRoute("GetPointInterest", new
            {
                cityName = cityName,
                pointInterest = finalPointInterest.Id
            }, finalPointInterest);
        }
    }
}
