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
                Pros = dto.Pros,
                GymStartTime = dto.GymStartTime,
                GymEndTime = dto.GymEndTime,
                PoolStartTime = dto.PoolStartTime,
                PoolEndTime = dto.PoolEndTime,
                HammamStartTime = dto.HammamStartTime,
                HammamEndTime = dto.HammamEndTime,
                TrainerWorkoutsPerWeek = dto.TrainerWorkoutsPerWeek,
            };
            _context.Tariffs.Add(tariff);
            await _context.SaveChangesAsync();

            return Ok(tariff);
        }

        [HttpPost("purchase")]
        public async Task <IActionResult> Purchase(TariffPurchaseDto dto)
        {
            var tariff = await _context.Tariffs.FirstOrDefaultAsync(x => x.Id == dto.TariffId);

            var userTariff = new UserTariff
            {
                UserId = dto.UserId,
                TariffId = dto.TariffId,
                AcquiredAt = DateTime.Now,
                ExpiresAt = (DateTime.Now).AddMonths(Convert.ToInt32(tariff.Period.Value.TotalHours)),
                MoneyPaid = dto.MoneyPaid
            };
            _context.UserTariffs.Add(userTariff);
            await _context.SaveChangesAsync();

            return Ok(userTariff);
        }
    }
}
