using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VKR2.DAL;
using VKR2.Domain;
using VKR2.Domain.DTOs;
using VKR2.DAL.Models;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly PostgresContext _context;

    public UsersController(PostgresContext context)
    {
        _context = context;
    }

    // GET: api/Users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsersDTO>>> GetUserses()
    {
        var users = await _context.Users
            .Select(u => new UsersDTO
            {
                Usercd = u.Usercd,
                Email = u.Email,
                Passwordhash = "", // Не возвращаем пароль даже в хешированном виде
                Parentcd = u.Parentcd,
                Workercd = u.Workercd
            }).ToListAsync();

        return Ok(users);
    }

    // GET: api/Users/5
    [HttpGet("{id}")]
    public async Task<ActionResult<UsersDTO>> GetUsers(int id)
    {
        var users = await _context.Users.FindAsync(id);
        if (users == null) return NotFound();

        return Ok(new UsersDTO
        {
            Usercd = users.Usercd,
            Email = users.Email,
            Passwordhash = "", // Не возвращаем пароль
            Parentcd = users.Parentcd,
            Workercd = users.Workercd
        });
    }

    // POST: api/Users
    [HttpPost]
    public async Task<ActionResult<UsersDTO>> CreateUsers(UsersCreateDTO dto)
    {
        if (string.IsNullOrEmpty(dto.Passwordhash))
            return BadRequest("Пароль обязателен");

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Passwordhash);

        var users = new User
        {
            Email = dto.Email,
            Passwordhash = hashedPassword,
            Parentcd = dto.Parentcd,
            Workercd = dto.Workercd
        };

        _context.Users.Add(users);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUsers), new { id = users.Usercd }, new UsersDTO
        {
            Usercd = users.Usercd,
            Email = users.Email,
            Passwordhash = "", // Не возвращаем пароль
            Parentcd = users.Parentcd,
            Workercd = users.Workercd
        });
    }

    // PUT: api/Users/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUsers(int id, UsersCreateDTO dto)
    {
        var user = await _context.Users
            .Include(u => u.Rolesusers)
            .FirstOrDefaultAsync(u => u.Usercd == id);

        if (user == null)
            return NotFound();

        // Обновляем email (с проверкой уникальности)
        if (!string.IsNullOrEmpty(dto.Email) && dto.Email != user.Email)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email && u.Usercd != id))
                return BadRequest("Email уже используется другим пользователем");

            user.Email = dto.Email;
        }

        // Обновляем пароль (если предоставлен)
        if (!string.IsNullOrEmpty(dto.Passwordhash))
        {
            if (dto.Passwordhash.StartsWith("$2a$") || dto.Passwordhash.StartsWith("$2b$") || dto.Passwordhash.StartsWith("$2y$"))
                return BadRequest("Необходимо предоставить чистый пароль, а не хеш");

            user.Passwordhash = BCrypt.Net.BCrypt.HashPassword(dto.Passwordhash);
        }

        // Обновляем роль (если изменилась)
        if (!string.IsNullOrEmpty(dto.RoleName))
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Rolename == dto.RoleName);
            if (role == null)
                return BadRequest("Указанная роль не существует");

            // Удаляем старые роли
            var oldRoles = _context.Rolesusers.Where(ru => ru.Usercd == id);
            _context.Rolesusers.RemoveRange(oldRoles);

            // Добавляем новую роль
            _context.Rolesusers.Add(new Rolesuser
            {
                Usercd = id,
                Rolecd = role.Rolecd
            });
        }

        // Обновляем остальные поля
        user.Parentcd = dto.Parentcd;
        user.Workercd = dto.Workercd;

        try
        {
            await _context.SaveChangesAsync();
            return NoContent();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Users.Any(e => e.Usercd == id))
                return NotFound();
            else
                throw;
        }
    }

    // DELETE: api/Users/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUsers(int id)
    {
        var users = await _context.Users.FindAsync(id);
        if (users == null) return NotFound();

        _context.Users.Remove(users);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}