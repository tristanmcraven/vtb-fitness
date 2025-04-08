using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vtb_fitness_api.Dto;
using vtb_fitness_api.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace vtb_fitness_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackerController : ControllerBase
    {
        private readonly VtbContext _context;
        public TrackerController(VtbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.Trackers.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(TrackerCreateDto dto)
        {
            var trackerRecord = new Tracker
            {
                UserId = dto.UserId,
                ExerciseId = dto.ExerciseId,
                Sits = dto.Sits,
                Reps = dto.Reps,
                Meters = dto.Meters,
                Timestamp = dto.TimeStamp ?? DateTime.Now,
            };
            _context.Trackers.Add(trackerRecord);
            await _context.SaveChangesAsync();

            return Ok(trackerRecord);
        }
    }
}
