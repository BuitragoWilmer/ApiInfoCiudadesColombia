using AutoMapper;
using InfoCity.API.Entities;
using InfoCity.API.Model;
using InfoCity.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoCity.API.Controllers
{
    [Route("api/ciudad/{cityName}/puntosdeinteres")]
    [ApiController]
    public class PointInterestController : ControllerBase
    {
        private readonly ILogger<PointInterestController> logger;
        private readonly IMailService mailservice;
        private readonly ICityInfoRepository citiesDataRepository;
        private readonly IMapper mapper;

        public PointInterestController(
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
        public async Task<ActionResult<IEnumerable<PointInterestDto>>> GetPointsInterest(string cityName)
        {
            try
            
            {
                if (!await citiesDataRepository.CityExistsAsync(cityName))
                {
                    logger.LogInformation($"La ciudad con nombre {cityName} no se encuentra en la base de datos, por favor crearla y registre los puntos de interes.");
                    return NotFound();
                }
                var pointInterestReturn = await citiesDataRepository.GetPointsOfInterestsForCityAsync(cityName);
                return  Ok(mapper.Map<IEnumerable<PointInterestDto>>(pointInterestReturn));
            }catch (Exception ex)
            {
                logger.LogCritical($"Excepcion causada en GetPointInterest con el parametro {cityName}", ex);
                return StatusCode(500, "Se ha generado un problema con la petición, intentelo más tarde.");
            }
            
        }

        [HttpGet("{pointInterest}", Name = "GetPointInterest")]
        public async Task<ActionResult<PointInterestDto>> GetPointInterest(string cityName, int pointInterest) {
            try
            {
                if (!await citiesDataRepository.CityExistsAsync(cityName))
                {
                    logger.LogInformation($"La ciudad con nombre {cityName} no se encuentra en la base de datos, por favor crearla y registre los puntos de interes.");
                    return NotFound();
                }
                var pointInterestReturn = await citiesDataRepository.GetPointOfInterestsForCityAsync(cityName, pointInterest);
                if (pointInterestReturn == null)
                {
                    return NotFound();
                }
                return Ok(mapper.Map<PointInterestDto>(pointInterestReturn));
            }
            catch (Exception ex)
            {
                logger.LogCritical($"Excepcion causada en GetPointInterest con el parametro {cityName}", ex);
                return StatusCode(500, "Se ha generado un problema con la petición, intentelo más tarde.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PointInterestDto>> CreatePointInterest(
            string cityName,
            PointInterestCreationDto pointInterest)
        {
            if (!await citiesDataRepository.CityExistsAsync(cityName))
            {
                return NotFound();
            }

            var finalPointInterest = mapper.Map<PointOfInterest>(pointInterest);

            await citiesDataRepository.AddPointInterestAsync(cityName, finalPointInterest);
            await citiesDataRepository.SaveChangesAsync();

            var pointInteresreturn = mapper.Map<PointInterestDto>(finalPointInterest);

            return CreatedAtRoute("GetPointInterest", new
            {
                cityName = cityName,
                pointInterest = pointInteresreturn.Id
            }, pointInteresreturn);
        }

        [HttpPut("{pointInterestId}")]
        public async Task<ActionResult> UpdatePointInterest(
            string cityName, int pointInterestId,
            PointInterestUpdateDto pointInterest)
        {
            if (!await citiesDataRepository.CityExistsAsync(cityName))
            {
                return NotFound();
            }

            var pointInterestStore = await citiesDataRepository.GetPointOfInterestsForCityAsync(cityName, pointInterestId);
            if (pointInterestStore == null)
            {
                return NotFound();
            }
            //cambia los atributos del origen con lo que tiene el destino
            mapper.Map(pointInterest, pointInterestStore);
            await citiesDataRepository.SaveChangesAsync();

            return NoContent();
        }


        [HttpPatch("{pointInterestId}")]
        public async Task<ActionResult> PartiallyUpdatePointInterest(
            string cityName, int pointInterestId,
            JsonPatchDocument<PointInterestUpdateDto> patchDocument)
        {
            if (!await citiesDataRepository.CityExistsAsync(cityName))
            {
                return NotFound();
            }

            var pointInterestStore = await citiesDataRepository.GetPointOfInterestsForCityAsync(cityName, pointInterestId);
            if (pointInterestStore == null)
            {
                return NotFound();
            }
            //Crea un objeto del tipo que recibe para poder hacerle el parcializado
            ///PointInterestUpdateDto pointInterestPatch = new PointInterestUpdateDto()
            ///{
            ///    Name = pointInterestStore.Name,
            ///    Description = pointInterestStore.Description
            ///};

            PointInterestUpdateDto pointInterestPatch = mapper.Map<PointInterestUpdateDto>(pointInterestStore);

            //Realiza el cambio al objeto 
            patchDocument.ApplyTo(pointInterestPatch, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (!TryValidateModel(pointInterestPatch))
            {
                return BadRequest(ModelState);
            }

            mapper.Map(pointInterestPatch, pointInterestStore);
            await citiesDataRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{pointInterestId}")]
        public async Task<ActionResult> DeletePointInterest(string cityName, int pointInterestId)
        {
            if (!await citiesDataRepository.CityExistsAsync(cityName))
            {
                return NotFound();
            }

            var pointInterestStore = await citiesDataRepository.GetPointOfInterestsForCityAsync(cityName, pointInterestId);
            if (pointInterestStore == null)
            {
                return NotFound();
            }
            citiesDataRepository.DeletePointInterest(pointInterestStore);
            await citiesDataRepository.SaveChangesAsync();

            mailservice.Send(
                    "Punto de interes eliminado",
                    $"punto de interes {pointInterestStore.Name} con id {pointInterestId}"
                );
            return NoContent();
        }
    }
}
