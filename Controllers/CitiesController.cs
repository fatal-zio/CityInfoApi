using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace CityInfoApi.Controllers
{
    public class CitiesController :  Controller
    {
        [HttpGet("api/cities")]
        public JsonResult GetCities()
        {
            return new JsonResult(new List<object>()
            {
                new {id=1, name="New York City"},
                new {id=2, name="Antwerp"}
            });
        }
    }

}