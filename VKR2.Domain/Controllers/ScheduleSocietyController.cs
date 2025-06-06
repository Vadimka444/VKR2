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
    public class ScheduleSocietyController : ControllerBase
    {
        private readonly PostgresContext _context;

        public ScheduleSocietyController(PostgresContext context)
        {
            _context = context;
        }


        [Authorize(Roles = "admin,worker,parent")]
        [HttpGet("schedule-societies")]
        public async Task<ActionResult<IEnumerable<ScheduleSocietyDTO>>> GetScheduleSocieties()
        {
            return await _context.Schedulesocieties
                .Select(ss => new ScheduleSocietyDTO
                {
                    Schedulecd = ss.Schedulecd,
                    Cabinetcd = ss.Cabinetcd,
                    Societycd = ss.Societycd,
                    Workercd = ss.Workercd,
                    Scheduledate = ss.Scheduledate,
                    StartTime = ss.StartTime,
                    EndTime = ss.EndTime
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ScheduleSocietyDTO>> GetScheduleSociety(int id)
        {
            var scheduleSociety = await _context.Schedulesocieties.FindAsync(id);
            if (scheduleSociety == null)
            {
                return NotFound();
            }

            return new ScheduleSocietyDTO
            {
                Schedulecd = scheduleSociety.Schedulecd,
                Cabinetcd = scheduleSociety.Cabinetcd,
                Societycd = scheduleSociety.Societycd,
                Workercd = scheduleSociety.Workercd,
                Scheduledate = scheduleSociety.Scheduledate,
                StartTime = scheduleSociety.StartTime,
                EndTime = scheduleSociety.EndTime
            };
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ScheduleSocietyDTO>> CreateScheduleSociety(ScheduleSocietyCreateDTO dto)
        {
            var scheduleSociety = new Schedulesociety
            {
                Cabinetcd = dto.Cabinetcd,
                Societycd = dto.Societycd,
                Workercd = dto.Workercd,
                Scheduledate = dto.Scheduledate,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime
            };

            _context.Schedulesocieties.Add(scheduleSociety);
            await _context.SaveChangesAsync();

            var result = new ScheduleSocietyDTO
            {
                Schedulecd = scheduleSociety.Schedulecd,
                Cabinetcd = scheduleSociety.Cabinetcd,
                Societycd = scheduleSociety.Societycd,
                Workercd = scheduleSociety.Workercd,
                Scheduledate = scheduleSociety.Scheduledate,
                StartTime = scheduleSociety.StartTime,
                EndTime = scheduleSociety.EndTime
            };

            return CreatedAtAction(nameof(GetScheduleSociety), new { id = scheduleSociety.Schedulecd }, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateScheduleSociety(int id, ScheduleSocietyCreateDTO dto)
        {
            var scheduleSociety = await _context.Schedulesocieties.FindAsync(id);
            if (scheduleSociety == null)
            {
                return NotFound();
            }

            scheduleSociety.Cabinetcd = dto.Cabinetcd;
            scheduleSociety.Societycd = dto.Societycd;
            scheduleSociety.Workercd = dto.Workercd;
            scheduleSociety.Scheduledate = dto.Scheduledate;
            scheduleSociety.StartTime = dto.StartTime;
            scheduleSociety.EndTime = dto.EndTime;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteScheduleSociety(int id)
        {
            var scheduleSociety = await _context.Schedulesocieties.FindAsync(id);
            if (scheduleSociety == null)
            {
                return NotFound();
            }

            _context.Schedulesocieties.Remove(scheduleSociety);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}