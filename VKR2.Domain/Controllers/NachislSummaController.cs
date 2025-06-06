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
    public class NachislSummaController : ControllerBase
    {
        private readonly PostgresContext _context;

        public NachislSummaController(PostgresContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<NachislSummaDTO>>> GetNachislSummas()
        {
            return await _context.Nachislsummas
                .Select(n => new NachislSummaDTO
                {
                    Nachislfactcd = n.Nachislfactcd,
                    Pupilcd = n.Pupilcd,
                    Societycd = n.Societycd,
                    Nachislsum = n.Nachislsum,
                    Nachislmonth = n.Nachislmonth,
                    Nachislyear = n.Nachislyear
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<NachislSummaDTO>> GetNachislSumma(int id)
        {
            var nachisl = await _context.Nachislsummas.FindAsync(id);
            if (nachisl == null)
            {
                return NotFound();
            }

            return new NachislSummaDTO
            {
                Nachislfactcd = nachisl.Nachislfactcd,
                Pupilcd = nachisl.Pupilcd,
                Societycd = nachisl.Societycd,
                Nachislsum = nachisl.Nachislsum,
                Nachislmonth = nachisl.Nachislmonth,
                Nachislyear = nachisl.Nachislyear
            };
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<NachislSummaDTO>> CreateNachislSumma(NachislSummaCreateDTO dto)
        {
            var nachisl = new Nachislsumma
            {
                Pupilcd = dto.Pupilcd,
                Societycd = dto.Societycd,
                Nachislsum = dto.Nachislsum,
                Nachislmonth = dto.Nachislmonth,
                Nachislyear = dto.Nachislyear
            };

            _context.Nachislsummas.Add(nachisl);
            await _context.SaveChangesAsync();

            var result = new NachislSummaDTO
            {
                Nachislfactcd = nachisl.Nachislfactcd,
                Pupilcd = nachisl.Pupilcd,
                Societycd = nachisl.Societycd,
                Nachislsum = nachisl.Nachislsum,
                Nachislmonth = nachisl.Nachislmonth,
                Nachislyear = nachisl.Nachislyear
            };

            return CreatedAtAction(nameof(GetNachislSumma), new { id = nachisl.Nachislfactcd }, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateNachislSumma(int id, NachislSummaCreateDTO dto)
        {
            var nachisl = await _context.Nachislsummas.FindAsync(id);
            if (nachisl == null)
            {
                return NotFound();
            }

            nachisl.Pupilcd = dto.Pupilcd;
            nachisl.Societycd = dto.Societycd;
            nachisl.Nachislsum = dto.Nachislsum;
            nachisl.Nachislmonth = dto.Nachislmonth;
            nachisl.Nachislyear = dto.Nachislyear;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteNachislSumma(int id)
        {
            var nachisl = await _context.Nachislsummas.FindAsync(id);
            if (nachisl == null)
            {
                return NotFound();
            }

            _context.Nachislsummas.Remove(nachisl);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}