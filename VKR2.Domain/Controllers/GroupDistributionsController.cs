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
    public class GroupDistributionsController : ControllerBase
    {
        private readonly PostgresContext _context;

        public GroupDistributionsController(PostgresContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "admin,worker")]
        public async Task<ActionResult<IEnumerable<GroupDistributionDTO>>> GetGroupDistributions()
        {
            return await _context.Groupdistributions
                .Select(gd => new GroupDistributionDTO
                {
                    Distributioncd = gd.Distributioncd,
                    Groupcd = gd.Groupcd,
                    Pupilcd = gd.Pupilcd
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin,worker")]
        public async Task<ActionResult<GroupDistributionDTO>> GetGroupDistribution(int id)
        {
            var distribution = await _context.Groupdistributions.FindAsync(id);
            if (distribution == null)
            {
                return NotFound();
            }

            return new GroupDistributionDTO
            {
                Distributioncd = distribution.Distributioncd,
                Groupcd = distribution.Groupcd,
                Pupilcd = distribution.Pupilcd
            };
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<GroupDistributionDTO>> CreateGroupDistribution(GroupDistributionCreateDTO dto)
        {
            var distribution = new Groupdistribution
            {
                Groupcd = dto.Groupcd,
                Pupilcd = dto.Pupilcd
            };

            _context.Groupdistributions.Add(distribution);
            await _context.SaveChangesAsync();

            var result = new GroupDistributionDTO
            {
                Distributioncd = distribution.Distributioncd,
                Groupcd = distribution.Groupcd,
                Pupilcd = distribution.Pupilcd
            };

            return CreatedAtAction(nameof(GetGroupDistribution), new { id = distribution.Distributioncd }, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateGroupDistribution(int id, GroupDistributionCreateDTO dto)
        {
            var distribution = await _context.Groupdistributions.FindAsync(id);
            if (distribution == null)
            {
                return NotFound();
            }

            distribution.Groupcd = dto.Groupcd;
            distribution.Pupilcd = dto.Pupilcd;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteGroupDistribution(int id)
        {
            var distribution = await _context.Groupdistributions.FindAsync(id);
            if (distribution == null)
            {
                return NotFound();
            }

            _context.Groupdistributions.Remove(distribution);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}