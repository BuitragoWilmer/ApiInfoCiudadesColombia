using InfoCity.API.DbContexts;
using InfoCity.API.Entities;
using InfoCity.API.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace InfoCity.API.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        public CityInfoRepository(CityInfoContext context)
        {
            Context = context;
        }

        public CityInfoContext Context { get; }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await Context.cities.OrderBy(x=>x.Name).ToListAsync();
        }

        /// <summary>
        /// Metodo de filtrado
        /// y busquedad
        /// </summary>
        public async Task<(IEnumerable<City>, PaginationMetadata)> GetCitiesAsync(string? name, string? searchQuery, int pageNumber , int pageSize)
        {
            //Coleccion de busqueda
            var cities = Context.cities as IQueryable<City>;

            //filtrado
            if (!string.IsNullOrWhiteSpace(name))
            {
                name = name.Trim();
                cities = cities.Where(x => x.Name == name); 
            }

            //busqueda
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                cities = cities.Where(x=>x.Name.Contains(searchQuery) || (x.Description != null && x.Description.Contains(searchQuery)));
            }
            
            var totalIteCount = await cities.CountAsync();
            var paginationMetadata = new PaginationMetadata(totalIteCount, pageSize, pageNumber);

            //coleccion paginada
            var citiesReturn = await cities.OrderBy(x => x.Name)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (citiesReturn, paginationMetadata);
        }

        public async Task<City> GetCityAsync(string cityName, bool includePointInterest)
        {
            if (includePointInterest)
            {
                return  await Context.cities.Include(x=>x.PointInterests).FirstOrDefaultAsync(x => x.Name==cityName);  
            }
            return await Context.cities.FirstOrDefaultAsync(x => x.Name==cityName);
        }

        public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestsForCityAsync(string cityName)
        {
            var ciudad = await Context.cities.FirstOrDefaultAsync(x => x.Name == cityName);
            return await Context.pointOfInterests.Where(x=>x.CityId == ciudad.CityId).ToListAsync();
        }

        public async Task<PointOfInterest> GetPointOfInterestsForCityAsync(string cityName, int pointInterestId) 
        {
            var ciudad = await Context.cities.FirstOrDefaultAsync(x => x.Name == cityName);
            return await Context.pointOfInterests.FirstOrDefaultAsync(x => x.CityId == ciudad.CityId && x.PointInterestId == pointInterestId);
        }
        public async Task<bool> CityExistsAsync(string cityName)
        {
            return await Context.cities.AnyAsync(x => x.Name == cityName);
        }

        public async Task AddPointInterestAsync(string cityName, PointOfInterest pointOfInterest)
        {
            var city = await GetCityAsync(cityName, false);
            if (city != null)
            {
                city.PointInterests.Add(pointOfInterest);   
            }

        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await Context.SaveChangesAsync() >= 0);
        }

        public void DeletePointInterest(PointOfInterest pointOfInterest)
        {
            Context.pointOfInterests.Remove(pointOfInterest);
        }
    }
}
