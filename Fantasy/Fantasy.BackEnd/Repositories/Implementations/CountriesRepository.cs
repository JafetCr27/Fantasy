using Fantasy.BackEnd.Data;
using Fantasy.BackEnd.Repositories.Interfaces;
using Fantasy.Shared.Entities;
using Fantasy.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace Fantasy.BackEnd.Repositories.Implementations;

public class CountriesRepository : GenericRepository<Country>, ICountriesRepository
{
    private readonly DataContext _context;
    public CountriesRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public async override Task<ActionResponse<Country>> GetAsync(int id)
    {
        var country = await _context.Countries
              .Include(c => c.Teams)
              .FirstOrDefaultAsync(c => c.Id == id);

        if (country == null)
        {
            return new ActionResponse<Country>
            {
                WasSuccess = false,
                Message = "ERR001"
            };
        }

        return new ActionResponse<Country>
        {
            WasSuccess = true,
            Result = country
        };
    }
    public async override Task<ActionResponse<IEnumerable<Country>>> GetAsync()
    {
        var countries = await _context.Countries
           .Include(c => c.Teams)
           .OrderBy(c => c.Name)
           .ToListAsync();
        return new ActionResponse<IEnumerable<Country>>
        {
            WasSuccess = true,
            Result = countries
        };
    }

    public async Task<IEnumerable<Country>> GetComboAsync()
    {
        return await _context.Countries
            .OrderBy(x => x.Name)
            .ToListAsync();
    }
}
