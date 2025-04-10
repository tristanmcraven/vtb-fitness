using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vtb_fitness_api.Dto;
using vtb_fitness_api.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace vtb_fitness_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly VtbContext _context;
        public ExerciseController(VtbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.Exercises.ToListAsync());
        }

        [HttpGet("name")]
        public async Task<IActionResult> GetByName(string name)
        {
            var e = await _context.Exercises.FirstOrDefaultAsync(x => x.Name == name);
            return e == null ? NotFound() : Ok(e);
        }

        [HttpGet("cardio")]
        public async Task<IActionResult> GetCardio()
        {
            return Ok(await _context.Exercises.Where(x => x.TypeId == 1).ToListAsync());
        }

        [HttpGet("strength")]
        public async Task<IActionResult> GetStrength()
        {
            return Ok(await _context.Exercises.Where(x => x.TypeId == 2 || x.TypeId == 3).ToListAsync());
        }

        [HttpGet("weight")]
        public async Task<IActionResult> GetWeight()
        {
            return Ok(await _context.Exercises.Where(x => x.TypeId == 4).ToListAsync());
        }

    }
}
