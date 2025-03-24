using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookSwap.Services;

namespace BookSwap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataSeederController : ControllerBase
    {
        private readonly DataSeeder _dataSeeder;
        public DataSeederController(DataSeeder dataSeeder)
        {
            _dataSeeder = dataSeeder;
        }


        [HttpPost]
        public IActionResult Seed()
        {
            _dataSeeder.Seed();
            return Ok("Data Seeded successfully");
        }
    }
}
