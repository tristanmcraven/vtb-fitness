using vtb_fitness_api.Dto;
using vtb_fitness_api.Model;
using vtb_fitness_api.Services.Interfaces;

namespace vtb_fitness_api.Services
{
    public class PassportService : IPassportService
    {
        private readonly VtbContext _context;

        public PassportService(VtbContext context) => _context = context;

        public async Task<Passport> Create(PassportCreateDto dto)
        {
            var passport = new Passport
            {
                Series = dto.PassportSeries,
                Number = dto.PassportNumber,
                IssuedBy = dto.IssuedBy,
                IssuedDate = dto.IssuedDate,
                UnitCode = dto.UnitCode,
                BirthDate = dto.BirthDate,
                BirthPlace = dto.BirthPlace,
            };

            _context.Passports.Add(passport);
            await _context.SaveChangesAsync();
            return passport;
        }
    }
}
