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
    public class AttendanceController : ControllerBase
    {
        private readonly PostgresContext _context;

        public AttendanceController(PostgresContext context) => _context = context;

        [HttpGet]
        [Authorize(Roles = "admin,worker")]
        public async Task<ActionResult<IEnumerable<AttendanceDTO>>> GetAttendances()
        {
            return await _context.Attendances
                .Select(a => new AttendanceDTO
                {
                    Visitingcd = a.Visitingcd,
                    Reasoncd = a.Reasoncd,
                    Pupilcd = a.Pupilcd,
                    Visitdate = a.Visitdate,
                    Availability = a.Availability
                }).ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<AttendanceDTO>> GetAttendance(int id)
        {
            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance == null) return NotFound();

            return new AttendanceDTO
            {
                Visitingcd = attendance.Visitingcd,
                Reasoncd = attendance.Reasoncd,
                Pupilcd = attendance.Pupilcd,
                Visitdate = attendance.Visitdate,
                Availability = attendance.Availability
            };
        }

        [HttpPost]
        [Authorize(Roles = "admin,worker")]
        public async Task<ActionResult<AttendanceDTO>> CreateAttendance(AttendanceCreateDTO dto)
        {
            var attendance = new Attendance
            {
                Reasoncd = dto.Reasoncd,
                Pupilcd = dto.Pupilcd,
                Visitdate = dto.Visitdate,
                Availability = dto.Availability
            };

            _context.Attendances.Add(attendance);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAttendance), new { id = attendance.Visitingcd }, new AttendanceDTO
            {
                Visitingcd = attendance.Visitingcd,
                Reasoncd = attendance.Reasoncd,
                Pupilcd = attendance.Pupilcd,
                Visitdate = attendance.Visitdate,
                Availability = attendance.Availability
            });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin,worker")]
        public async Task<IActionResult> UpdateAttendance(int id, AttendanceCreateDTO dto)
        {
            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance == null) return NotFound();

            attendance.Reasoncd = dto.Reasoncd;
            attendance.Pupilcd = dto.Pupilcd;
            attendance.Visitdate = dto.Visitdate;
            attendance.Availability = dto.Availability;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin,worker")]
        public async Task<IActionResult> DeleteAttendance(int id)
        {
            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance == null) return NotFound();

            _context.Attendances.Remove(attendance);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}