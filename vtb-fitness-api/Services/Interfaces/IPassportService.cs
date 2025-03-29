using vtb_fitness_api.Dto;
using vtb_fitness_api.Model;

namespace vtb_fitness_api.Services.Interfaces
{
    public interface IPassportService
    {
        Task<Passport> Create(PassportCreateDto dto);
    }
}
