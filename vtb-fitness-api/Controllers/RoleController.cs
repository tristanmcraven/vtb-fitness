using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vtb_fitness_api.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace vtb_fitness_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly VtbContext _context;
        public RoleController(VtbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _context.Roles.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(x => x.Id == id);
            return role == null ? NotFound() : Ok(role);
        }
    }
}
