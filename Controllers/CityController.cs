using AutoMapper;
using InfoCity.API.Entities;
using InfoCity.API.Model;
using InfoCity.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace InfoCity.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/ciudad")]
    public class CityController: ControllerBase
    {
        private readonly ILogger<PointInterestController> logger;
        private readonly IMailService mailservice;
        private readonly ICityInfoRepository citiesDataRepository;
        private readonly IMapper mapper;
        const int maxCitiesPageSize = 15;

        public CityController(
            ILogger<PointInterestController> logger, 
            IMailService mailservice, 
            ICityInfoRepository citiesDataRepository,
            IMapper mapper)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.mailservice = mailservice ?? throw new ArgumentNullException(nameof(mailservice));
            this.citiesDataRepository = citiesDataRepository ?? throw new ArgumentNullException(nameof(citiesDataRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<(IEnumerable<CityWithoutPointInterestDto>, PaginationMetadata)>> 
            GetCities(string? name, string?searchQuery, int pageNumber=1, int pageSize=10)
        {
            try
            {
                if(pageSize > maxCitiesPageSize)
                {
                    pageSize = maxCitiesPageSize;
                }
                (IEnumerable<City> cityEntities , PaginationMetadata paginationMetadata) = await citiesDataRepository
                    .GetCitiesAsync(name, searchQuery, pageNumber, pageSize);

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

                return Ok(mapper.Map<IEnumerable<CityWithoutPointInterestDto>>(cityEntities));

            }catch (Exception ex)
            {
                logger.LogCritical($"Excepcion causada en GetCities", ex);
                return StatusCode(500, "Se ha generado un problema con la petición, intentelo más tarde.");
            }
        }

        [HttpGet("{nameCity}")]
        public async Task<ActionResult> GetCity(string nameCity, bool includePointInteres = false)
        {
            try
            {
                City cityEntities = await citiesDataRepository.GetCityAsync(nameCity, includePointInteres);
                if (cityEntities == null)
                {
                    return NotFound();
                }
                if (includePointInteres)
                {
                    return Ok(mapper.Map<CityDto>(cityEntities));
                }
                return Ok(mapper.Map<CityWithoutPointInterestDto>(cityEntities));
            }
            catch (Exception ex)
            {
                logger.LogCritical($"Excepcion causada en GetCity", ex);
                return StatusCode(500, "Se ha generado un problema con la petición, intentelo más tarde.");
            }
        }
    }
}
