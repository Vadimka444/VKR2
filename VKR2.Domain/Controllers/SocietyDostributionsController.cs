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
    public class SocietyDistributionsController : ControllerBase
    {
        private readonly PostgresContext _context;

        public SocietyDistributionsController(PostgresContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "admin,worker")]
        public async Task<ActionResult<IEnumerable<SocietyDistributionDTO>>> GetSocietyDistributions()
        {
            return await _context.Societydistributions
                .Select(sd => new SocietyDistributionDTO
                {
                    Distributioncd = sd.Distributioncd,
                    Pupilcd = sd.Pupilcd,
                    Societycd = sd.Societycd
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin,worker")]
        public async Task<ActionResult<SocietyDistributionDTO>> GetSocietyDistribution(int id)
        {
            var societyDistribution = await _context.Societydistributions.FindAsync(id);
            if (societyDistribution == null)
            {
                return NotFound();
            }

            return new SocietyDistributionDTO
            {
                Distributioncd = societyDistribution.Distributioncd,
                Pupilcd = societyDistribution.Pupilcd,
                Societycd = societyDistribution.Societycd
            };
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<SocietyDistributionDTO>> CreateSocietyDistribution(SocietyDistributionCreateDTO dto)
        {
            var societyDistribution = new Societydistribution
            {
                Pupilcd = dto.Pupilcd,
                Societycd = dto.Societycd
            };

            _context.Societydistributions.Add(societyDistribution);
            await _context.SaveChangesAsync();

            var result = new SocietyDistributionDTO
            {
                Distributioncd = societyDistribution.Distributioncd,
                Pupilcd = societyDistribution.Pupilcd,
                Societycd = societyDistribution.Societycd
            };

            return CreatedAtAction(nameof(GetSocietyDistribution), new { id = societyDistribution.Distributioncd }, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateSocietyDistribution(int id, SocietyDistributionCreateDTO dto)
        {
            var societyDistribution = await _context.Societydistributions.FindAsync(id);
            if (societyDistribution == null)
            {
                return NotFound();
            }

            societyDistribution.Pupilcd = dto.Pupilcd;
            societyDistribution.Societycd = dto.Societycd;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteSocietyDistribution(int id)
        {
            var societyDistribution = await _context.Societydistributions.FindAsync(id);
            if (societyDistribution == null)
            {
                return NotFound();
            }

            _context.Societydistributions.Remove(societyDistribution);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}