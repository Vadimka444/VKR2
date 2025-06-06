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
    public class PupilsController : ControllerBase
    {
        private readonly PostgresContext _context;

        public PupilsController(PostgresContext context) => _context = context;

        [HttpGet]
        [Authorize(Roles = "admin,worker")]
        public async Task<ActionResult<IEnumerable<PupilDTO>>> GetPupils()
        {
            return await _context.Pupils
                .Select(p => new PupilDTO
                {
                    Pupilcd = p.Pupilcd,
                    Fio = p.Fio,
                    Birthcertificatenumber = p.Birthcertificatenumber,
                    Gender = p.Gender,
                    Dateofbirth = p.Dateofbirth
                }).ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin,worker")]
        public async Task<ActionResult<PupilDTO>> GetPupil(int id)
        {
            var pupil = await _context.Pupils.FindAsync(id);
            if (pupil == null) return NotFound();

            return new PupilDTO
            {
                Pupilcd = pupil.Pupilcd,
                Fio = pupil.Fio,
                Birthcertificatenumber = pupil.Birthcertificatenumber,
                Gender = pupil.Gender,
                Dateofbirth = pupil.Dateofbirth
            };
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<PupilDTO>> CreatePupil(PupilCreateDTO dto)
        {
            var pupil = new Pupil
            {
                Fio = dto.Fio,
                Birthcertificatenumber = dto.Birthcertificatenumber,
                Gender = dto.Gender,
                Dateofbirth = dto.Dateofbirth
            };

            _context.Pupils.Add(pupil);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPupil), new { id = pupil.Pupilcd }, new PupilDTO
            {
                Pupilcd = pupil.Pupilcd,
                Fio = pupil.Fio,
                Birthcertificatenumber = pupil.Birthcertificatenumber,
                Gender = pupil.Gender,
                Dateofbirth = pupil.Dateofbirth
            });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdatePupil(int id, PupilCreateDTO dto)
        {
            var pupil = await _context.Pupils.FindAsync(id);
            if (pupil == null) return NotFound();

            pupil.Fio = dto.Fio;
            pupil.Birthcertificatenumber = dto.Birthcertificatenumber;
            pupil.Gender = dto.Gender;
            pupil.Dateofbirth = dto.Dateofbirth;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeletePupil(int id)
        {
            var pupil = await _context.Pupils.FindAsync(id);
            if (pupil == null) return NotFound();

            _context.Pupils.Remove(pupil);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}