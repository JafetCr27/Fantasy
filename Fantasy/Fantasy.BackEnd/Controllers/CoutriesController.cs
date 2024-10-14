namespace Fantasy.BackEnd.Controllers
{
    using Fantasy.BackEnd.Data;
    using Fantasy.Shared.Entities;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]
    public class CoutriesController : ControllerBase
    {
        private readonly DataContext _context;

        public CoutriesController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _context.Countries.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCountryAsync(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return BadRequest();
            }
            return Ok(country);
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync(Country country)
        {
            _context.Add(country);
            await _context.SaveChangesAsync();
            return Ok(country);
        }
        [HttpPut]
        public async Task<IActionResult> PutAsync(Country country)
        {
            var currentCountry = await _context.Countries.FindAsync(country.Id);
            if (currentCountry == null)
            {
                return BadRequest();
            }
            currentCountry.Name = country.Name;
            _context.Update(country);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return BadRequest();
            }
            _context.Remove(country);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
