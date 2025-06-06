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
    public class ReasonsController : ControllerBase
    {
        private readonly PostgresContext _context;

        public ReasonsController(PostgresContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "admin,worker,parent")]
        public async Task<ActionResult<IEnumerable<ReasonDTO>>> GetReasons()
        {
            return await _context.Reasons
                .Select(r => new ReasonDTO
                {
                    Reasoncd = r.Reasoncd,
                    Title = r.Title
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ReasonDTO>> GetReason(int id)
        {
            var reason = await _context.Reasons.FindAsync(id);
            if (reason == null)
            {
                return NotFound();
            }

            return new ReasonDTO
            {
                Reasoncd = reason.Reasoncd,
                Title = reason.Title
            };
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ReasonDTO>> CreateReason(ReasonCreateDTO dto)
        {
            var reason = new Reason
            {
                Title = dto.Title
            };

            _context.Reasons.Add(reason);
            await _context.SaveChangesAsync();

            var result = new ReasonDTO
            {
                Reasoncd = reason.Reasoncd,
                Title = reason.Title
            };

            return CreatedAtAction(nameof(GetReason), new { id = reason.Reasoncd }, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateReason(int id, ReasonCreateDTO dto)
        {
            var reason = await _context.Reasons.FindAsync(id);
            if (reason == null)
            {
                return NotFound();
            }

            reason.Title = dto.Title;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteReason(int id)
        {
            var reason = await _context.Reasons.FindAsync(id);
            if (reason == null)
            {
                return NotFound();
            }

            _context.Reasons.Remove(reason);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}