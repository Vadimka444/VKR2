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
    public class GroupsController : ControllerBase
    {
        private readonly PostgresContext _context;

        public GroupsController(PostgresContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "admin,worker")]
        public async Task<ActionResult<IEnumerable<GroupDTO>>> GetGroups()
        {
            return await _context.Groups
                .Select(g => new GroupDTO
                {
                    Groupcd = g.Groupcd,
                    Title = g.Title,
                    Maxage = g.Maxage,
                    Minage = g.Minage,
                    Numberofseats = g.Numberofseats
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin,worker")]
        public async Task<ActionResult<GroupDTO>> GetGroup(int id)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group == null)
            {
                return NotFound();
            }

            return new GroupDTO
            {
                Groupcd = group.Groupcd,
                Title = group.Title,
                Maxage = group.Maxage,
                Minage = group.Minage,
                Numberofseats = group.Numberofseats
            };
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<GroupDTO>> CreateGroup(GroupCreateDTO dto)
        {
            var group = new Group
            {
                Title = dto.Title,
                Maxage = dto.Maxage,
                Minage = dto.Minage,
                Numberofseats = dto.Numberofseats
            };

            _context.Groups.Add(group);
            await _context.SaveChangesAsync();

            var result = new GroupDTO
            {
                Groupcd = group.Groupcd,
                Title = group.Title,
                Maxage = group.Maxage,
                Minage = group.Minage,
                Numberofseats = group.Numberofseats
            };

            return CreatedAtAction(nameof(GetGroup), new { id = group.Groupcd }, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateGroup(int id, GroupCreateDTO dto)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group == null)
            {
                return NotFound();
            }

            group.Title = dto.Title;
            group.Maxage = dto.Maxage;
            group.Minage = dto.Minage;
            group.Numberofseats = dto.Numberofseats;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group == null)
            {
                return NotFound();
            }

            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}