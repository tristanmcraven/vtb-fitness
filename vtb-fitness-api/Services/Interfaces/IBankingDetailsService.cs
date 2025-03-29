using vtb_fitness_api.Dto;
using vtb_fitness_api.Model;

namespace vtb_fitness_api.Services.Interfaces
{
    public interface IBankingDetailsService
    {
        Task<BankingDetail> Create(BankingDetailsCreateDto dto);
    }
}
