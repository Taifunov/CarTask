using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarTestTask.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CarTestTask.Services
{
#nullable enable
    public class MongoDBCarService : IMongoDBCarService
    {
        private readonly IMongoCollection<Car> _cars;

        public MongoDBCarService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _cars = database.GetCollection<Car>("Cars") ?? throw new ArgumentNullException(nameof(_cars));
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
            await _cars.InsertOneAsync(carToCreate);
            return carToCreate;
        }

        public async Task<List<Car>> GetAllAsync() =>
            await _cars.Find(car => true)
                .ToListAsync();

        public async Task<Car> FindAsync(string id)
        {
            var result = await _cars.Find(car => car.Id == id)
               .FirstOrDefaultAsync();
            return result;
        }

        private async Task Update(CarDTO car)
        {
            Car carToUpdate = await FindAsync(car.Id);
            UpdateDefinition<Car>? update = null;
            if (!string.IsNullOrEmpty(car.Name))
            {
                update = Builders<Car>.Update.Set(c => c.Name, car.Name);
            }

            if (car.Description != "")
            {
                update = update == null
                    ? Builders<Car>.Update.Set(c => c.Description, car.Description)
                    : update.Set(c => c.Description, car.Description);
            }
            var filter = new BsonDocument("_id", car.Id);
            await _cars.UpdateOneAsync(filter, update);
        }

        public async Task<Car> CreateOrUpdateAsync(CarDTO car)
        {
            Car result = null;
            if (car.Id == null)
            {
                result = await Create(car);
            } else
            {
                await Update(car);
            }
            return result;
        }

        public async Task DeleteAsync(string id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            await _cars.DeleteOneAsync(car => car.Id == id);
        }
    }
}
