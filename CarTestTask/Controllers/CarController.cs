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
        private readonly IMongoDBCarService _mongoCarService;
        private readonly IInMemoryCarService _inMemoryCarService;

        public CarController(IMongoDBCarService mongoCarService, IInMemoryCarService inMemoryCarService)
        {
            _mongoCarService = mongoCarService;
            _inMemoryCarService = inMemoryCarService;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Car>>> Get()
        {
            var cars = await _mongoCarService.GetAllAsync();
            
            return Ok(cars);
        }

        [HttpGet("{id}", Name = "GetCar")]
        public async Task<ActionResult<Car>> FindByIdAsync([FromRoute] string id)
        {
            var car = await _mongoCarService.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }

        [HttpDelete("{id}")]
        public async Task DeleteCardAsync([FromRoute] string id) =>
            await _mongoCarService.DeleteAsync(id);


        [HttpPost]
        public async Task<ActionResult> CreateOrUpdateAsync([FromBody] CarDTO model)
        {
            await _mongoCarService.CreateOrUpdateAsync(model);

            return CreatedAtRoute(nameof(FindByIdAsync), new { id = model.Id }, model);
        }

        [HttpPost("CreateOrUpdateForTest")]
        public async Task<Car> CreateOrUpdateForTest([FromBody] CarDTO car)
        {
            return await _inMemoryCarService.CreateOrUpdateAsync(car);
        }

        [HttpGet("GetAllTest")]
        public async Task<List<Car>> GetTest()
        {
            var result = await _inMemoryCarService.GetAllAsync();

            return result;
        }
    }
}
