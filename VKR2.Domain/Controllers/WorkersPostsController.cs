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
    public class WorkersPostsController : ControllerBase
    {
        private readonly PostgresContext _context;

        public WorkersPostsController(PostgresContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "admin,worker")]
        public async Task<ActionResult<IEnumerable<WorkerPostDTO>>> GetWorkerPosts()
        {
            return await _context.Workersposts
                .Select(wp => new WorkerPostDTO
                {
                    WorkerPostcd = wp.Workerpostcd,
                    Workercd = wp.Workercd,
                    Postcd = wp.Postcd
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<WorkerPostDTO>> GetWorkerPost(int id)
        {
            var workerPost = await _context.Workersposts.FindAsync(id);
            if (workerPost == null)
            {
                return NotFound();
            }

            return new WorkerPostDTO
            {
                WorkerPostcd = workerPost.Workerpostcd,
                Workercd = workerPost.Workercd,
                Postcd = workerPost.Postcd
            };
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<WorkerPostDTO>> CreateWorkerPost(WorkerPostCreateDTO dto)
        {
            var workerPost = new Workerspost
            {
                Workercd = dto.Workercd,
                Postcd = dto.Postcd
            };

            _context.Workersposts.Add(workerPost);
            await _context.SaveChangesAsync();

            var result = new WorkerPostDTO
            {
                WorkerPostcd = workerPost.Workerpostcd,
                Workercd = workerPost.Workercd,
                Postcd = workerPost.Postcd
            };

            return CreatedAtAction(nameof(GetWorkerPost), new { id = workerPost.Workerpostcd }, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateWorkerPost(int id, WorkerPostCreateDTO dto)
        {
            var workerPost = await _context.Workersposts.FindAsync(id);
            if (workerPost == null)
            {
                return NotFound();
            }

            workerPost.Workercd = dto.Workercd;
            workerPost.Postcd = dto.Postcd;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteWorkerPost(int id)
        {
            var workerPost = await _context.Workersposts.FindAsync(id);
            if (workerPost == null)
            {
                return NotFound();
            }

            _context.Workersposts.Remove(workerPost);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}