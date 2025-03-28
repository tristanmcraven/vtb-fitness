using Microsoft.AspNetCore.Mvc;
using vtb_fitness_api.Dto;
using vtb_fitness_api.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace vtb_fitness_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankingDetailsController : ControllerBase
    {
        private readonly VtbContext _context;

        public BankingDetailsController(VtbContext context) => _context = context;

        [HttpPost("create")]
        public async Task<IActionResult> Create(BankingDetailsCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var details = new BankingDetail
            {
                CorrespondentAccount = dto.CorrespondentAccount,
                Bik = dto.Bik,
                BankName = dto.BankName,
                DebitCardNumber = dto.DebitCardNumber
            };

            _context.BankingDetails.Add(details);
            await _context.SaveChangesAsync();

            return Ok(details);
        }
    }
}
