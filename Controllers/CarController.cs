using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
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
        public async Task<IActionResult> AddCar(Car car)
        {
            await carRepository.Insert(car);
            return Json(car.Id);
        }

        [HttpPut("{id}@{version}")]
        [SwaggerOperation(Summary = "Update car filtered by id.")]
        public async Task UpdateCar(string id, string version, Car car)
        {
            await carRepository.Update(id, version, car);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Get car filtered by id.")]
        public async Task<IActionResult> DeleteById(string id)
        {
            DeleteResult result = await carRepository.DeleteById(id);
            return Json(result.DeletedCount == 1);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get car filtered by id.")]
        public async Task<IActionResult> GetById(string id)
        {
            Car car = await carRepository.GetById(id);

            if (car == null) 
            {
                return NotFound();
            }

            return Json(car);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all cars.")]
        public async Task<IActionResult> List([FromQuery] Pagination pagination)
        {
            IEnumerable<Car> cars = await carRepository.List(pagination);
            return Json(cars);
        }
    }
}