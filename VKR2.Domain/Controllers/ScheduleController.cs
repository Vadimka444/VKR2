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
    public class ScheduleController : ControllerBase
    {
        private readonly PostgresContext _context;

        public ScheduleController(PostgresContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "admin,worker, parent")]
        public async Task<ActionResult<IEnumerable<ScheduleDTO>>> GetSchedules()
        {
            return await _context.Schedules
                .Select(s => new ScheduleDTO
                {
                    Schedulecd = s.Schedulecd,
                    Cabinetcd = s.Cabinetcd,
                    Groupcd = s.Groupcd,
                    Lessoncd = s.Lessoncd,
                    Workercd = s.Workercd,
                    Scheduledate = s.Scheduledate,
                    StartTime = s.StartTime,
                    EndTime = s.EndTime
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ScheduleDTO>> GetSchedule(int id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }

            return new ScheduleDTO
            {
                Schedulecd = schedule.Schedulecd,
                Cabinetcd = schedule.Cabinetcd,
                Groupcd = schedule.Groupcd,
                Lessoncd = schedule.Lessoncd,
                Workercd = schedule.Workercd,
                Scheduledate = schedule.Scheduledate,
                StartTime = schedule.StartTime,
                EndTime = schedule.EndTime
            };
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ScheduleDTO>> CreateSchedule(ScheduleCreateDTO dto)
        {
            var schedule = new Schedule
            {
                Cabinetcd = dto.Cabinetcd,
                Groupcd = dto.Groupcd,
                Lessoncd = dto.Lessoncd,
                Workercd = dto.Workercd,
                Scheduledate = dto.Scheduledate,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime
            };

            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();

            var result = new ScheduleDTO
            {
                Schedulecd = schedule.Schedulecd,
                Cabinetcd = schedule.Cabinetcd,
                Groupcd = schedule.Groupcd,
                Lessoncd = schedule.Lessoncd,
                Workercd = schedule.Workercd,
                Scheduledate = schedule.Scheduledate,
                StartTime = schedule.StartTime,
                EndTime = schedule.EndTime
            };

            return CreatedAtAction(nameof(GetSchedule), new { id = schedule.Schedulecd }, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateSchedule(int id, ScheduleCreateDTO dto)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }

            schedule.Cabinetcd = dto.Cabinetcd;
            schedule.Groupcd = dto.Groupcd;
            schedule.Lessoncd = dto.Lessoncd;
            schedule.Workercd = dto.Workercd;
            schedule.Scheduledate = dto.Scheduledate;
            schedule.StartTime = dto.StartTime;
            schedule.EndTime = dto.EndTime;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }

            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}