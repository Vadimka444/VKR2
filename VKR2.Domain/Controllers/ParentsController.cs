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
    public class ParentsController : ControllerBase
    {
        private readonly PostgresContext _context;

        public ParentsController(PostgresContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "admin,worker")]
        public async Task<ActionResult<IEnumerable<ParentDTO>>> GetParents()
        {
            return await _context.Parents
                .Select(p => new ParentDTO
                {
                    Parentcd = p.Parentcd,
                    Fio = p.Fio,
                    Passport = p.Passport,
                    Phone = p.Phone,
                    Address = p.Address
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin,worker,parent")]
        public async Task<ActionResult<ParentDTO>> GetParent(int id)
        {
            var parent = await _context.Parents.FindAsync(id);
            if (parent == null)
            {
                return NotFound();
            }

            return new ParentDTO
            {
                Parentcd = parent.Parentcd,
                Fio = parent.Fio,
                Passport = parent.Passport,
                Phone = parent.Phone,
                Address = parent.Address
            };
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ParentDTO>> CreateParent(ParentCreateDTO dto)
        {
            var parent = new Parent
            {
                Fio = dto.Fio,
                Passport = dto.Passport,
                Phone = dto.Phone,
                Address = dto.Address
            };

            _context.Parents.Add(parent);
            await _context.SaveChangesAsync();

            var result = new ParentDTO
            {
                Parentcd = parent.Parentcd,
                Fio = parent.Fio,
                Passport = parent.Passport,
                Phone = parent.Phone,
                Address = parent.Address
            };

            return CreatedAtAction(nameof(GetParent), new { id = parent.Parentcd }, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin, parent")]
        public async Task<IActionResult> UpdateParent(int id, ParentCreateDTO dto)
        {
            var parent = await _context.Parents.FindAsync(id);
            if (parent == null)
            {
                return NotFound();
            }

            parent.Fio = dto.Fio;
            parent.Passport = dto.Passport;
            parent.Phone = dto.Phone;
            parent.Address = dto.Address;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteParent(int id)
        {
            var parent = await _context.Parents.FindAsync(id);
            if (parent == null)
            {
                return NotFound();
            }

            _context.Parents.Remove(parent);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}