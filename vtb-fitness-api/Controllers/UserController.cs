using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using vtb_fitness_api.Dto;
using vtb_fitness_api.Model;
using vtb_fitness_api.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace vtb_fitness_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly VtbContext _context;
        private readonly IBankingDetailsService _bankingDetailsService;
        private readonly IPassportService _passportService;
        public UserController(VtbContext context, IBankingDetailsService bankingDetailsService, IPassportService passportService)
        {
            _context = context;
            _bankingDetailsService = bankingDetailsService;
            _passportService = passportService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        [HttpPost("sign_in")]
        public async Task<IActionResult> SignIn(UserSignInDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == dto.Login);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return Unauthorized();

            return Ok(user);
        }

        [HttpPost("sign_up")]
        public async Task<IActionResult> SignUp(UserSignUpDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var passport = await _passportService.Create(dto.PassportCreateDto);
            var bankingDetails = dto.BankingDetailsDto != null ? await _bankingDetailsService.Create(dto.BankingDetailsDto) : null;

            var user = new User
            {
                Lastname = dto.Lastname,
                Name = dto.Name,
                Middlename = dto.Middlename,
                Phone = dto.Phone,
                Email = dto.Email,
                Pfp = dto.Pfp,
                RoleId = dto.RoleId,
                CreatedAt = DateTime.Now,
                WorkingInVtbSince = dto.WorkingInVtbSince,
                Login = dto.Login,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                PassportId = passport.Id,
                BankingDetailsId = bankingDetails != null ? bankingDetails.Id : null
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        [HttpGet("{id}/current_tariff")]
        public async Task<IActionResult> GetCurrentTariff(int id)
        {
            var tariff = await _context.UserTariffs.Where(x => x.UserId == id && x.ExpiresAt > DateTime.Now).Include(x => x.Tariff).FirstOrDefaultAsync();
            return tariff != null ? Ok(tariff) : NotFound();
        }

        //awful
        [HttpGet("search/users")]
        public async Task<IActionResult> SearchUsers(string? q)
        {
            if (String.IsNullOrWhiteSpace(q))
                return Ok(await _context.Users.Where(x => x.RoleId == 3).ToListAsync());
            q = q.ToLower();
            return Ok(await _context.Users.Where(x => (x.Lastname + x.Name + x.Middlename).ToLower().Contains(q) && x.RoleId == 3).Include(x => x.Role).ToListAsync());
        }

        [HttpGet("search/mods")]
        public async Task<IActionResult> SearchMods(string? q)
        {
            if (String.IsNullOrWhiteSpace(q))
                return Ok(await _context.Users.Where(x => x.RoleId == 2).ToListAsync());
            q = q.ToLower();
            return Ok(await _context.Users.Where(x => (x.Lastname + x.Name + x.Middlename).ToLower().Contains(q) && x.RoleId == 2).Include(x => x.Role).ToListAsync());
        }

        [HttpGet("search/trainers")]
        public async Task<IActionResult> SearchTrainers(string? q)
        {
            if (String.IsNullOrWhiteSpace(q))
                return Ok(await _context.Users.Where(x => x.RoleId == 4).ToListAsync());
            q = q.ToLower();
            return Ok(await _context.Users.Where(x => (x.Lastname + x.Name + x.Middlename).ToLower().Contains(q) && x.RoleId == 4).Include(x => x.Role).ToListAsync());
        }

        [HttpGet("{id}/tracker")]
        public async Task<IActionResult> GetTracker(int id)
        {
            return Ok(await _context.Trackers.Where(t => t.UserId == id).ToListAsync());
        }
    }
}
