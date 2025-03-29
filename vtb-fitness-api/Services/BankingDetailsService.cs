using vtb_fitness_api.Dto;
using vtb_fitness_api.Model;
using vtb_fitness_api.Services.Interfaces;

namespace vtb_fitness_api.Services
{
    public class BankingDetailsService : IBankingDetailsService
    {
        private readonly VtbContext _context;
        public BankingDetailsService(VtbContext context) => _context = context;

        public async Task<BankingDetail> Create(BankingDetailsCreateDto dto)
        {
            var details = new BankingDetail
            {
                CorrespondentAccount = dto.CorrespondentAccount,
                Bik = dto.Bik,
                BankName = dto.BankName,
                DebitCardNumber = dto.DebitCardNumber
            };

            _context.BankingDetails.Add(details);
            await _context.SaveChangesAsync();
            return details;
        }
    }
}
