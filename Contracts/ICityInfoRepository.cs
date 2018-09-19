using System.Collections.Generic;
using CityInfoApi.Entities;

namespace CityInfoApi.Contracts
{
    public interface ICityInfoRepository
    {
         IEnumerable<City> GetCities();
         City GetCity(int cityId, bool includePointsOfInterest);
         IEnumerable<PointOfInterest> GetPointsOfInterestForCity(int cityId);
         PointOfInterest GetPointOfInterestForCity(int cityId, int pointOfInterestId);
    }
}