using System.Collections.Generic;
using System.Linq;
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
            var results = new List<CityWithoutPointsOfInterestDto>();

            foreach (var cityEntity in cityEntities)
            {
                results.Add(new CityWithoutPointsOfInterestDto
                {
                    Id = cityEntity.Id,
                    Description = cityEntity.Description,
                    Name = cityEntity.Name
                });
            }

            return Ok(results);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id, bool includePointsOfInterest = false)
        {
            var city = _cityInfoRepository.GetCity(id, includePointsOfInterest);
            
            if (city == null) {
                return NotFound();
            }

            if (includePointsOfInterest) {

                CityDto cityWithPointsOfInterestResult =
                    new CityDto()
                    {
                            Id = city.Id,
                            Name = city.Name,
                            Description = city.Description
                    };

                foreach (var poi in city.PointsOfInterest)
                {
                    cityWithPointsOfInterestResult.PointsOfInterest.Add(
                        new PointOfInterestDto()
                        {
                            Id = poi.Id,
                            Name = poi.Name,
                            Description = poi.Description
                        }
                    );
                }

                return Ok(cityWithPointsOfInterestResult);
            }

            var cityResult = new CityWithoutPointsOfInterestDto()
            {
                Id = city.Id,
                Name = city.Name,
                Description = city.Description
            };

            return Ok(cityResult);
        }
    }

}