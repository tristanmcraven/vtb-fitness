using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vtb_fitness_api.Dto;
using vtb_fitness_api.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace vtb_fitness_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TariffController : ControllerBase
    {
        private readonly VtbContext _context;
        public TariffController(VtbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.Tariffs.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(TariffCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tariff = new Tariff
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Period = dto.Period,
                Pros = dto.Pros
            };
            _context.Tariffs.Add(tariff);
            await _context.SaveChangesAsync();

            return Ok(tariff);
        }
    }
}
