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
    public class CabinetsController : ControllerBase
    {
        private readonly PostgresContext _context;

        public CabinetsController(PostgresContext context) => _context = context;

        [HttpGet]
        [Authorize(Roles = "admin,worker, parent")]
        public async Task<ActionResult<IEnumerable<CabinetDTO>>> GetCabinets()
        {
            return await _context.Cabinets
                .Select(c => new CabinetDTO
                {
                    Cabinetcd = c.Cabinetcd,
                    Cabinetno = c.Cabinetno,
                    Location = c.Location
                }).ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin,worker")]
        public async Task<ActionResult<CabinetDTO>> GetCabinet(int id)
        {
            var cabinet = await _context.Cabinets.FindAsync(id);
            if (cabinet == null) return NotFound();

            return new CabinetDTO
            {
                Cabinetcd = cabinet.Cabinetcd,
                Cabinetno = cabinet.Cabinetno,
                Location = cabinet.Location
            };
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<CabinetDTO>> CreateCabinet(CabinetCreateDTO dto)
        {
            var cabinet = new Cabinet
            {
                Cabinetno = dto.Cabinetno,
                Location = dto.Location
            };

            _context.Cabinets.Add(cabinet);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCabinet), new { id = cabinet.Cabinetcd }, new CabinetDTO
            {
                Cabinetcd = cabinet.Cabinetcd,
                Cabinetno = cabinet.Cabinetno,
                Location = cabinet.Location
            });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateCabinet(int id, CabinetCreateDTO dto)
        {
            var cabinet = await _context.Cabinets.FindAsync(id);
            if (cabinet == null) return NotFound();

            cabinet.Cabinetno = dto.Cabinetno;
            cabinet.Location = dto.Location;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteCabinet(int id)
        {
            var cabinet = await _context.Cabinets.FindAsync(id);
            if (cabinet == null) return NotFound();

            _context.Cabinets.Remove(cabinet);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}