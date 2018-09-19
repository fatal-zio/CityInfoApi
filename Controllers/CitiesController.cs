using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CityInfoApi.Contracts;
using CityInfoApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfoApi.Controllers
{
    [Route("api/cities")]
    public class CitiesController :  Controller
    {
        private ICityInfoRepository _cityInfoRepository;

        public CitiesController(ICityInfoRepository cityInfoRepository) => _cityInfoRepository = cityInfoRepository;

        [HttpGet()]
        public IActionResult GetCities()
        {
            var cityEntities = _cityInfoRepository.GetCities();

            return Ok(Mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cityEntities));
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id, bool includePointsOfInterest = false)
        {
            if (!_cityInfoRepository.CityExists(id)) {
                return NotFound();
            }

            var city = _cityInfoRepository.GetCity(id, includePointsOfInterest);

            return includePointsOfInterest ?
                 Ok(Mapper.Map<CityDto>(city)) :
                Ok(Mapper.Map<CityWithoutPointsOfInterestDto>(city));
        }
    }

}