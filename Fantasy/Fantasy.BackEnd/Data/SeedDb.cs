
using Fantasy.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fantasy.BackEnd.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            // Crea la db si no existe
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
            await CheckTeamsAsync();
        }

        private async Task CheckTeamsAsync()
        {
            if (!_context.Teams.Any())
            {
                foreach (var country in _context.Countries)
                {
                    _context.Teams.Add(new Team { Name = country.Name, Country = country! });
                    if (country.Name == "Costa Rica")
                    {
                        _context.Teams.Add(new Team { Name = "Saprissa", Country = country! });
                        _context.Teams.Add(new Team { Name = "Alajuelense", Country = country! });
                        _context.Teams.Add(new Team { Name = "Herediano", Country = country! });
                    }
                }
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                var conuntriesSQLScript = File.ReadAllText("Data\\Countries.sql");
                await _context.Database.ExecuteSqlRawAsync(conuntriesSQLScript);
            }
        }
    }
}
