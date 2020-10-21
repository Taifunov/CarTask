using System.Collections.Generic;
using System.Threading.Tasks;
using CarTestTask.Models;
using CarTestTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarTestTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly IMongoDBCarService _carRepository;
        private readonly IInMemoryCarService _inMemoryCarRepository;

        public CarController(IMongoDBCarService carRepository, IInMemoryCarService inMemoryCarRepository)
        {
            _carRepository = carRepository;
            _inMemoryCarRepository = inMemoryCarRepository;
        }

        [HttpGet("getAll")]
        public async Task<IList<Car>> Get()
        {
            return await _carRepository.GetAllAsync();
        }

        [HttpGet("find/{id}", Name = "GetCar")]
        public async Task<Car> FindByIdAsync([FromRoute] string id)
        {
            return await _carRepository.FindAsync(id);
        }

        [HttpDelete("{id}")]
        public async Task DeleteCardAsync([FromRoute] string id) =>
            await _carRepository.DeleteAsync(id);


        [HttpPost("CreateOrUpdate")]
        public async Task<ActionResult> CreateOrUpdateAsync([FromBody] CarDTO model)
        {
            await _carRepository.CreateorUpdateAsync(model);

            if (model != null)
            {
                return CreatedAtRoute("GetCar", new { id = model.Id }, model);
            }
            return Ok();
        }

        [HttpPost("CreateOrUpdateForTest")]
        public async Task<Car> CreateOrUpdateForTest([FromBody] CarDTO car)
        {
            return await _inMemoryCarRepository.CreateOrUpdateAsync(car);
        }

        [HttpGet("GetAllTest")]
        public async Task<List<Car>> GetTest()
        {
            var result = await _inMemoryCarRepository.GetAllAsync();

            return result;
        }
    }
}
