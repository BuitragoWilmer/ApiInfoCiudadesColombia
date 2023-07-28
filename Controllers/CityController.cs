using AutoMapper;
using InfoCity.API.Attributes;
using InfoCity.API.Entities;
using InfoCity.API.Model;
using InfoCity.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace InfoCity.API.Controllers
{
    [Route("api/v{version:apiVersion}/ciudad")]
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Produces("application/json", "application/xml")]
    //Permite separar las especificaciones de cada API
    [ApiExplorerSettings(GroupName = "InfoCityAPI")]
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

        /// <summary>
        /// Obtiene todas las ciudad permitiendo paginación
        /// </summary>
        /// <param name="name">Filro por nombre de la ciudad</param>
        /// <param name="searchQuery">Parametro para busqueda en los campos de descripcion o Nombre</param>
        /// <param name="pageNumber">Numero de paginas para la paginacion</param>
        /// <param name="pageSize">Tamaño de la pagina</param>
        /// <returns>Lista de ciudades</returns>
        [RequestHeaderMatchesMediaType("Accept", 
            "application/json", 
            "application/vnd.marvin.CityDto+json")]
        [Produces("application/vnd.marvin.CityDto+json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CityDto))]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [HttpGet(Name = "GetCities")]
        public async Task<ActionResult<IEnumerable<CityDto>>>
            GetCitiesWithPointInterest(string name, string searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                if (pageSize > maxCitiesPageSize)
                {
                    pageSize = maxCitiesPageSize;
                }
                (IEnumerable<City> cityEntities, PaginationMetadata paginationMetadata) = await citiesDataRepository
                    .GetCitiesAsync(name, searchQuery, pageNumber, pageSize);

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

                return Ok(mapper.Map<IEnumerable<CityDto>>(cityEntities));

            }
            catch (Exception ex)
            {
                logger.LogCritical($"Excepcion causada en GetCities", ex);
                return StatusCode(500, "Se ha generado un problema con la petición, intentelo más tarde.");
            }
        }

        /// <summary>
        /// Obtiene todas las ciudad permitiendo paginación
        /// </summary>
        /// <param name="name">Filtro por nombre de la ciudad</param>
        /// <param name="searchQuery">Parametro para busqueda en los campos de descripcion o Nombre</param>
        /// <param name="pageNumber">Numero de paginas para la paginacion</param>
        /// <param name="pageSize">Tamaño de la pagina</param>
        /// <returns>Lista de ciudades</returns>
        [RequestHeaderMatchesMediaType("Accept",
            "application/vnd.marvin.CityWithoutPointInterestDto+json")]
        [Produces("application/vnd.marvin.CityWithoutPointInterestDto+json")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<IEnumerable<CityWithoutPointInterestDto>>> 
            GetCities(string name, string searchQuery, int pageNumber=1, int pageSize=10)
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

        /// <summary>
        /// Obtiene una ciudad especifica por su nombre
        /// </summary>
        /// <param name="nameCity">Nombre de la ciudad</param>
        /// <param name="includePointInteres">Si se requiere que se incluyan los puntos de interes asociados a la ciudad</param>
        /// <returns>Ciudad</returns>
        /// <response code="200">Devuelve la ciudad dependiendo si se requiere con puntos de interes o no</response>
        /// <response code="404">No se encontro la ciudad en la base de datos</response>

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CityWithoutPointInterestDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{nameCity}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
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
