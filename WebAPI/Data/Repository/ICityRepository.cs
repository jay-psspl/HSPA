using WebAPI.Models;

namespace WebAPI.Data.Repository
{
    public interface ICityRepository
    {
         Task<IEnumerable<City>> GetCitiesAsync();
         
         void AddCity(City city);

         void DeleteCity(int CityId);

         Task<bool> SaveAsync();
    }
}