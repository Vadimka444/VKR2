using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using VKR2.Domain.DTOs;
using VKR2.DAL.Models;
using VKR2.DAL;


namespace VKR2.Domain.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SocietyController : ControllerBase
    {
        private readonly PostgresContext _context;

        public SocietyController(PostgresContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "admin,worker,parent")]
        public async Task<ActionResult<IEnumerable<SocietyDTO>>> GetSocieties()
        {
            return await _context.Societies
                .Select(s => new SocietyDTO
                {
                    Societycd = s.Societycd,
                    Title = s.Title,
                    Maxage = s.Maxage,
                    Minage = s.Minage,
                    Numberofseats = s.Numberofseats,
                    Price = s.Price
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin,worker")]
        public async Task<ActionResult<SocietyDTO>> GetSociety(int id)
        {
            var society = await _context.Societies.FindAsync(id);
            if (society == null)
            {
                return NotFound();
            }

            return new SocietyDTO
            {
                Societycd = society.Societycd,
                Title = society.Title,
                Maxage = society.Maxage,
                Minage = society.Minage,
                Numberofseats = society.Numberofseats,
                Price = society.Price
            };
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<SocietyDTO>> CreateSociety(SocietyCreateDTO dto)
        {
            var society = new Society
            {
                Title = dto.Title,
                Maxage = dto.Maxage,
                Minage = dto.Minage,
                Numberofseats = dto.Numberofseats,
                Price = dto.Price
            };

            _context.Societies.Add(society);
            await _context.SaveChangesAsync();

            var result = new SocietyDTO
            {
                Societycd = society.Societycd,
                Title = society.Title,
                Maxage = society.Maxage,
                Minage = society.Minage,
                Numberofseats = society.Numberofseats,
                Price = society.Price
            };

            return CreatedAtAction(nameof(GetSociety), new { id = society.Societycd }, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateSociety(int id, SocietyCreateDTO dto)
        {
            var society = await _context.Societies.FindAsync(id);
            if (society == null)
            {
                return NotFound();
            }

            society.Title = dto.Title;
            society.Maxage = dto.Maxage;
            society.Minage = dto.Minage;
            society.Numberofseats = dto.Numberofseats;
            society.Price = dto.Price;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteSociety(int id)
        {
            var society = await _context.Societies.FindAsync(id);
            if (society == null)
            {
                return NotFound();
            }

            _context.Societies.Remove(society);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}