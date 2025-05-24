using Fantasy.Shared.Entities;
using Fantasy.Shared.Responses;

namespace Fantasy.BackEnd.Repositories.Interfaces
{
    public interface ICountriesRepository
    {
        Task<ActionResponse<Country>> GetAsync(int id);
        Task<ActionResponse<IEnumerable<Country>>> GetAsync();
        Task<IEnumerable<Country>> GetComboAsync();
    }
}
