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
    public class CurriculumController : ControllerBase
    {
        private readonly PostgresContext _context;

        public CurriculumController(PostgresContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "admin,worker, parent")]
        public async Task<ActionResult<IEnumerable<CurriculumDTO>>> GetCurricula()
        {
            return await _context.Curricula
                .Select(c => new CurriculumDTO
                {
                    Lessoncd = c.Lessoncd,
                    Title = c.Title,
                    Duration = c.Duration,
                    Quantity = c.Quantity
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin,worker")]
        public async Task<ActionResult<CurriculumDTO>> GetCurriculum(int id)
        {
            var curriculum = await _context.Curricula.FindAsync(id);
            if (curriculum == null)
            {
                return NotFound();
            }

            return new CurriculumDTO
            {
                Lessoncd = curriculum.Lessoncd,
                Title = curriculum.Title,
                Duration = curriculum.Duration,
                Quantity = curriculum.Quantity
            };
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<CurriculumDTO>> CreateCurriculum(CurriculumCreateDTO dto)
        {
            var curriculum = new Curriculum
            {
                Title = dto.Title,
                Duration = dto.Duration,
                Quantity = dto.Quantity
            };

            _context.Curricula.Add(curriculum);
            await _context.SaveChangesAsync();

            var result = new CurriculumDTO
            {
                Lessoncd = curriculum.Lessoncd,
                Title = curriculum.Title,
                Duration = curriculum.Duration,
                Quantity = curriculum.Quantity
            };

            return CreatedAtAction(nameof(GetCurriculum), new { id = curriculum.Lessoncd }, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateCurriculum(int id, CurriculumCreateDTO dto)
        {
            var curriculum = await _context.Curricula.FindAsync(id);
            if (curriculum == null)
            {
                return NotFound();
            }

            curriculum.Title = dto.Title;
            curriculum.Duration = dto.Duration;
            curriculum.Quantity = dto.Quantity;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteCurriculum(int id)
        {
            var curriculum = await _context.Curricula.FindAsync(id);
            if (curriculum == null)
            {
                return NotFound();
            }

            _context.Curricula.Remove(curriculum);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}