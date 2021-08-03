using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using telstra.demo.Models;
using telstra.demo.Repositories;

namespace telstra.demo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : Controller
    {
        private readonly IMongoRepository<Car> carRepository;

        public CarController(IMongoRepository<Car> carRepository)
        {
            this.carRepository = carRepository;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Add new car.")]
        public async Task AddCar(Car car)
        {
            await carRepository.Insert(car);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update car filtered by id.")]
        public async Task UpdateCar(string id, Car car)
        {
            await carRepository.Update(id, car);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Get car filtered by id.")]
        public async Task DeleteById(string id)
        {
            await carRepository.DeleteById(id);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get car filtered by id.")]
        public async Task<IActionResult> GetById(string id)
        {
            Car car = await carRepository.GetById(id);

            if (car == null) 
            {
                return NoContent();
            }

            return Json(car);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all cars.")]
        public async Task<IActionResult> List()
        {
            IEnumerable<Car> cars = await carRepository.List();
            return Json(cars);
        }
    }
}