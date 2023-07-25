using InfoCity.API.Entities;
using InfoCity.API.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfoCity.API.Services
{
    public interface ICityInfoRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();

        Task<(IEnumerable<City>, PaginationMetadata)> 
            GetCitiesAsync(string? cityName, string? searchQuery, int pageNumber, int pageSize);

        Task<City?> GetCityAsync(string cityName, bool includePointInterest);

        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestsForCityAsync(string cityName);

        Task<PointOfInterest?> GetPointOfInterestsForCityAsync(string cityName, int pointInterestId);

        Task<bool> CityExistsAsync(string cityName);

        Task AddPointInterestAsync(string cityName, PointOfInterest pointOfInterest);

        void DeletePointInterest(PointOfInterest pointOfInterest);

        Task<bool> SaveChangesAsync();
    }
}
