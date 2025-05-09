using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vtb_fitness_api.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace vtb_fitness_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecController : ControllerBase
    {
        private readonly VtbContext _context;

        public SpecController(VtbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.Specs.ToListAsync());
        }

        [HttpGet("{specId}")]
        public async Task<IActionResult> GetById(int specId)
        {
            var spec = await _context.Specs.FirstOrDefaultAsync(x => x.Id == specId);
            return spec != null ? Ok(spec) : NotFound();
        }
    }
}
