using System.Collections.Generic;
using System.Threading.Tasks;
using CarTestTask.Models;

namespace CarTestTask.Services
{
    public interface IInMemoryCarService
    {
        Task<Car> CreateOrUpdateAsync(CarDTO car);
        Task<List<Car>> GetAllAsync();
        Task<Car> FindAsync(string id);
    }
}
