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
    public class FamilyConnectionsController : ControllerBase
    {
        private readonly PostgresContext _context;

        public FamilyConnectionsController(PostgresContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<FamilyConnectionDTO>>> GetFamilyConnections()
        {
            return await _context.Familyconnections
                .Select(f => new FamilyConnectionDTO
                {
                    Familyconnectioncd = f.Familyconnectioncd,
                    Parentcd = f.Parentcd,
                    Pupilcd = f.Pupilcd
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<FamilyConnectionDTO>> GetFamilyConnection(int id)
        {
            var connection = await _context.Familyconnections.FindAsync(id);
            if (connection == null)
            {
                return NotFound();
            }

            return new FamilyConnectionDTO
            {
                Familyconnectioncd = connection.Familyconnectioncd,
                Parentcd = connection.Parentcd,
                Pupilcd = connection.Pupilcd
            };
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<FamilyConnectionDTO>> CreateFamilyConnection(FamilyConnectionCreateDTO dto)
        {
            var connection = new Familyconnection
            {
                Parentcd = dto.Parentcd,
                Pupilcd = dto.Pupilcd
            };

            _context.Familyconnections.Add(connection);
            await _context.SaveChangesAsync();

            var result = new FamilyConnectionDTO
            {
                Familyconnectioncd = connection.Familyconnectioncd,
                Parentcd = connection.Parentcd,
                Pupilcd = connection.Pupilcd
            };

            return CreatedAtAction(nameof(GetFamilyConnection), new { id = connection.Familyconnectioncd }, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateFamilyConnection(int id, FamilyConnectionCreateDTO dto)
        {
            var connection = await _context.Familyconnections.FindAsync(id);
            if (connection == null)
            {
                return NotFound();
            }

            connection.Parentcd = dto.Parentcd;
            connection.Pupilcd = dto.Pupilcd;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteFamilyConnection(int id)
        {
            var connection = await _context.Familyconnections.FindAsync(id);
            if (connection == null)
            {
                return NotFound();
            }

            _context.Familyconnections.Remove(connection);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("extended")]
        [Authorize(Roles = "admin,worker")]
        public async Task<ActionResult<IEnumerable<ExtendedFamilyConnectionDTO>>> GetExtendedFamilyConnections()
        {
            return await _context.Familyconnections
                .Include(fc => fc.Parent)
                .Include(fc => fc.Pupil)
                    .ThenInclude(p => p.Groupdistributions)
                        .ThenInclude(gd => gd.Group)
                .Select(fc => new ExtendedFamilyConnectionDTO
                {
                    Familyconnectioncd = fc.Familyconnectioncd,
                    Pupilcd = fc.Pupilcd,
                    PupilFio = fc.Pupil.Fio,
                    Parentcd = fc.Parentcd,
                    ParentFio = fc.Parent.Fio,
                    KinshipTitle = fc.Kinshipdegree ?? "Не указано", // Берем степень родства из Familyconnections
                    ParentPhone = fc.Parent.Phone ?? "Не указан",
                    GroupTitle = fc.Pupil.Groupdistributions
                        .OrderByDescending(gd => gd.Distributioncd)
                        .FirstOrDefault() != null
                            ? fc.Pupil.Groupdistributions
                                .OrderByDescending(gd => gd.Distributioncd)
                                .FirstOrDefault().Group.Title
                            : "Не распределен"
                })
                .ToListAsync();
        }
    }
}