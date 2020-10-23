using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarTestTask.Models;
using CarTestTask.Tests.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace CarTestTask.Services
{
    public class InMemoryCarService : IInMemoryCarService
    {
        private readonly DataContext _context;

        public InMemoryCarService(DataContext context)
        {
            _context = context;
        }
        private async Task<Car> Create(CarDTO car)
        {
            if (string.IsNullOrEmpty(car.Name))
                throw new ArgumentNullException("Name is null or empty");

            var id = Guid.NewGuid().ToString();

            Car carToCreate = new Car
            {
                Id = id,
                Name = car.Name,
                Description = car.Description
            };

            var result = await _context.Cars.AddAsync(carToCreate);

            await _context.SaveChangesAsync();

            return result.Entity;
        }

        private async Task<Car> Update(CarDTO car)
        {
            Car carToUpdate = await FindAsync(car.Id);

            if (!string.IsNullOrEmpty(car.Name))
            {
                carToUpdate.Name = car.Name;
            }

            if (car.Description != "")
            {
                carToUpdate.Description = car.Description;
            }

            var result =  _context.Cars.Update(carToUpdate);

            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<List<Car>> GetAllAsync() =>
            await _context.Cars.ToListAsync();

        public async Task<Car> FindAsync(string id)
        {
            var result = await _context.Cars
                .FirstOrDefaultAsync(car => car.Id == id);

            return result;
        }

        public async Task<Car> CreateOrUpdateAsync(CarDTO car)
        {
            Car result = null;
            if (car.Id == null)
            {
               result =  await Create(car);
            } else
            {
               result =  await Update(car);
            }

            _context.SaveChanges();
            return result;
        }

        public async Task DeleteAsync(string id)
        {
            var currentCar = await FindAsync(id);

            if (currentCar == null)
               throw new ArgumentNullException(nameof(currentCar));
            

            _context.Cars.Remove(currentCar);

            await _context.SaveChangesAsync();
        }
    }
}