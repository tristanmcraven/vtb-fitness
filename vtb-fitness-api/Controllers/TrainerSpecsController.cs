using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using vtb_fitness_api.Dto;
using vtb_fitness_api.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace vtb_fitness_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerSpecsController : ControllerBase
    {
        private readonly VtbContext _context;

        public TrainerSpecsController(VtbContext context) => _context = context;

        [HttpPost]
        public async Task<IActionResult> Post(TrainerSpecPostDto dto)
        {
            var trainer = await _context.Users.Include(x => x.Specs).FirstAsync(x => x.Id == dto.TrainerId);
            trainer.Specs.Clear();

            var newSpecs = await _context.Specs.Where(x => dto.SpecIds.Contains(x.Id)).ToListAsync();
            foreach (var spec in newSpecs)
            {
                trainer.Specs.Add(spec);
            }
            await _context.SaveChangesAsync();

            return Ok(newSpecs);

        }
    }
}
