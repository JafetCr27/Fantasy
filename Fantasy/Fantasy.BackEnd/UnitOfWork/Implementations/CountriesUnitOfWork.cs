using Fantasy.BackEnd.Repositories.Interfaces;
using Fantasy.BackEnd.UnitOfWork.Interfaces;
using Fantasy.Shared.Entities;
using Fantasy.Shared.Responses;

namespace Fantasy.BackEnd.UnitOfWork.Implementations;

public class CountriesUnitOfWork : GenericUnitOfWork<Country>, ICountriesUnitOfWork
{
    private readonly ICountriesRepository _countriesRepository;

    public CountriesUnitOfWork(IGenericRepository<Country> repository, ICountriesRepository countriesRepository) : base(repository)
    {
        _countriesRepository = countriesRepository;
    }
    public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync() => await
        _countriesRepository.GetAsync();

    public override async Task<ActionResponse<Country>> GetAsync(int id) => await _countriesRepository.GetAsync(id);

    public async Task<IEnumerable<Country>> GetComboAsync() => await _countriesRepository.GetComboAsync();

}
