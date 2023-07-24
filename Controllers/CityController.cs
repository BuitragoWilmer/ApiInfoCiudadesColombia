using InfoCity.API.Model;
using InfoCity.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InfoCity.API.Controllers
{
    [ApiController]
    [Route("api/ciudad")]
    public class CityController: ControllerBase
    {
        private readonly ILogger<PointInterestController> logger;
        private readonly IMailService mailservice;
        private readonly CitiesDataStore citiesData;
        public CityController(ILogger<PointInterestController> logger, IMailService mailservice, CitiesDataStore citiesData)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.mailservice = mailservice ?? throw new ArgumentNullException(nameof(mailservice));
            this.citiesData = citiesData ?? throw new ArgumentNullException(nameof(citiesData));
            ;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> GetCities()
        {
            return Ok(citiesData.Cities);
        }

        [HttpGet("{name}")]
        public ActionResult<CityDto> GetCity(string name)
        {
            CityDto city = citiesData.Cities.FirstOrDefault(x => x.Name == name);
            if(city == null)
            {
                return NotFound();
            }
            return Ok(city);
        }
    }
}
