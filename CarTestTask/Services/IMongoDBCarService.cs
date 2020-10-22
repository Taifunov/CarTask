using System.Collections.Generic;
using System.Threading.Tasks;
using CarTestTask.Models;

namespace CarTestTask.Services
{
    public interface IMongoDBCarService
    {
        Task<List<Car>> GetAllAsync();
        Task<Car> FindAsync(string id);
        Task<Car> CreateOrUpdateAsync(CarDTO model);
        Task DeleteAsync(string id);
    }
}