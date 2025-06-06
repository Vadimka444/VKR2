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
    public class PostsController : ControllerBase
    {
        private readonly PostgresContext _context;

        public PostsController(PostgresContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "admin,worker")]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetPosts()
        {
            return await _context.Posts
                .Select(p => new PostDTO
                {
                    Postcd = p.Postcd,
                    Title = p.Title
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<PostDTO>> GetPost(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            return new PostDTO
            {
                Postcd = post.Postcd,
                Title = post.Title
            };
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<PostDTO>> CreatePost(PostCreateDTO dto)
        {
            var post = new Post
            {
                Title = dto.Title
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            var result = new PostDTO
            {
                Postcd = post.Postcd,
                Title = post.Title
            };

            return CreatedAtAction(nameof(GetPost), new { id = post.Postcd }, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdatePost(int id, PostCreateDTO dto)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            post.Title = dto.Title;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}