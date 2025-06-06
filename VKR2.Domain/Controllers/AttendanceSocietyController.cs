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
    public class AttendanceSocietyController : ControllerBase
    {
        private readonly PostgresContext _context;

        public AttendanceSocietyController(PostgresContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "admin,worker,parent")]
        public async Task<ActionResult<IEnumerable<AttendanceSocietyDTO>>> GetAttendanceSocieties()
        {
            return await _context.Attendancesocieties
                .Select(a => new AttendanceSocietyDTO
                {
                    Visitingcd = a.Visitingcd,
                    Pupilcd = a.Pupilcd,
                    Societycd = a.Societycd,
                    Reasoncd = a.Reasoncd,
                    Visitdate = a.Visitdate,
                    Availability = a.Availability
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<AttendanceSocietyDTO>> GetAttendanceSociety(int id)
        {
            var attendance = await _context.Attendancesocieties.FindAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }

            return new AttendanceSocietyDTO
            {
                Visitingcd = attendance.Visitingcd,
                Pupilcd = attendance.Pupilcd,
                Societycd = attendance.Societycd,
                Reasoncd = attendance.Reasoncd,
                Visitdate = attendance.Visitdate,
                Availability = attendance.Availability
            };
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<AttendanceSocietyDTO>> CreateAttendanceSociety(AttendanceSocietyCreateDTO dto)
        {
            var attendance = new Attendancesociety
            {
                Pupilcd = dto.Pupilcd,
                Societycd = dto.Societycd,
                Reasoncd = dto.Reasoncd,
                Visitdate = dto.Visitdate,
                Availability = dto.Availability
            };

            _context.Attendancesocieties.Add(attendance);
            await _context.SaveChangesAsync();

            var result = new AttendanceSocietyDTO
            {
                Visitingcd = attendance.Visitingcd,
                Pupilcd = attendance.Pupilcd,
                Societycd = attendance.Societycd,
                Reasoncd = attendance.Reasoncd,
                Visitdate = attendance.Visitdate,
                Availability = attendance.Availability
            };

            return CreatedAtAction(nameof(GetAttendanceSociety), new { id = attendance.Visitingcd }, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateAttendanceSociety(int id, AttendanceSocietyCreateDTO dto)
        {
            var attendance = await _context.Attendancesocieties.FindAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }

            attendance.Pupilcd = dto.Pupilcd;
            attendance.Societycd = dto.Societycd;
            attendance.Reasoncd = dto.Reasoncd;
            attendance.Visitdate = dto.Visitdate;
            attendance.Availability = dto.Availability;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteAttendanceSociety(int id)
        {
            var attendance = await _context.Attendancesocieties.FindAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }

            _context.Attendancesocieties.Remove(attendance);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}