using System.Collections.Generic;
using System.Linq;
using CityInfoApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfoApi.Contracts
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private CityInfoContext _context;

        public CityInfoRepository(CityInfoContext context) => _context = context;

        public void AddPointOfInterestForCity(int cityId, PointOfInterest pointOfInterest)
        {
            var city = GetCity(cityId, false);
            city.PointsOfInterest.Add(pointOfInterest);
        }

        public bool CityExists(int cityId) => _context.Cities.Any(o => o.Id == cityId);

        public IEnumerable<City> GetCities() => _context.Cities.OrderBy(o => o.Name).ToList();

        public City GetCity(int cityId, bool includePointsOfInterest) => 
            includePointsOfInterest ?
                _context.Cities.Include(o => o.PointsOfInterest).FirstOrDefault(c => c.Id == cityId) :
                _context.Cities.FirstOrDefault(o => o.Id == cityId);


        public PointOfInterest GetPointOfInterestForCity(int cityId, int pointOfInterestId) => 
            _context.PointsOfInterest.FirstOrDefault(o => o.CityId == cityId && o.Id == pointOfInterestId);

        public IEnumerable<PointOfInterest> GetPointsOfInterestForCity(int cityId) => 
            _context.PointsOfInterest.Where(o => o.CityId == cityId).ToList();

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}