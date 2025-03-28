using Microsoft.AspNetCore.Mvc;
using vtb_fitness_api.Dto;
using vtb_fitness_api.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace vtb_fitness_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassportController : ControllerBase
    {
        private readonly VtbContext _context;

        public PassportController(VtbContext context) => _context = context;

        [HttpPost("create")]
        public async Task<IActionResult> Create(PassportCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var passport = new Passport
            {
                Series = dto.PassportSeries,
                Number = dto.PassportNumber,
                IssuedBy = dto.IssuedBy,
                IssuedDate = dto.IssuedDate,
                UnitCode = dto.UnitCode,
                BirthDate = dto.BirthDate,
                BirthPlace = dto.BirthPlace
            };
            _context.Passports.Add(passport);
            await _context.SaveChangesAsync();

            return Ok(passport);
        }
    }
}
