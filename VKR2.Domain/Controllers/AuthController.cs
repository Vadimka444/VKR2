using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using BCrypt.Net;
using VKR2.Domain.DTOs;
using VKR2.DAL.Models;
using VKR2.DAL;

namespace VKR2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly PostgresContext _context;
    private readonly IConfiguration _configuration;

    public AuthController(PostgresContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    private async Task<Role> GetRole(string roleName)
    {
        var role = await _context.Roles.FirstOrDefaultAsync(r => r.Rolename == roleName);
        if (role == null)
            throw new Exception($"Роль '{roleName}' не найдена");
        return role;
    }

    [HttpPost("register/parent")]
    public async Task<IActionResult> RegisterParent([FromBody] RegisterParentDto dto)
    {
        if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            return BadRequest(new { message = "Пользователь с таким Email уже существует" });

        var parent = await _context.Parents.FirstOrDefaultAsync(p => p.Fio == dto.Fio);
        if (parent == null)
        {
            parent = new Parent
            {
                Fio = dto.Fio,
                Passport = dto.passport,
                Phone = dto.phone,
                Address = dto.address
            };
            _context.Parents.Add(parent);
            await _context.SaveChangesAsync();
        }

        var hash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var user = new User
        {
            Email = dto.Email,
            Passwordhash = hash,
            Parentcd = parent.Parentcd
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var parentRole = await GetRole("parent");
        var userRole = new Rolesuser
        {
            Rolecd = parentRole.Rolecd,
            Usercd = user.Usercd
        };
        _context.Rolesusers.Add(userRole);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Родитель успешно зарегистрирован" });
    }

    [HttpPost("register/worker")]
    public async Task<IActionResult> RegisterWorker([FromBody] RegisterWorkerDto dto)
    {
        if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            return BadRequest("Пользователь с таким Email уже существует");

        var worker = await _context.Workers.FirstOrDefaultAsync(w => w.Fio == dto.Fio);
        if (worker == null)
        {
            worker = new Worker
            {
                Fio = dto.Fio,
                Phone = dto.phone,
                Passport = dto.passport,
                Address = dto.address,
                Dateofbirth = dto.dateofbirth
            };
            _context.Workers.Add(worker);
            await _context.SaveChangesAsync();
        }

        var hash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var user = new User
        {
            Email = dto.Email,
            Passwordhash = hash,
            Workercd = worker.Workercd
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var workerRole = await GetRole("worker");
        var userRole = new Rolesuser
        {
            Rolecd = workerRole.Rolecd,
            Usercd = user.Usercd
        };
        _context.Rolesusers.Add(userRole);
        await _context.SaveChangesAsync();

        return Ok("Сотрудник успешно зарегистрирован");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await _context.Users
            .Include(u => u.Rolesusers)
            .ThenInclude(ru => ru.Role)
            .FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Passwordhash))
            return Unauthorized("Неверный логин или пароль");

        var roleName = user.Rolesusers.FirstOrDefault()?.Role?.Rolename ?? "User";

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email, ClaimValueTypes.String),
            new Claim(ClaimTypes.NameIdentifier, user.Usercd.ToString(), ClaimValueTypes.String),
            new Claim(ClaimTypes.Role, roleName, ClaimValueTypes.String)
        };

        if (user.Parentcd.HasValue)
            claims.Add(new Claim("ParentCd", user.Parentcd.Value.ToString(), ClaimValueTypes.String));

        if (user.Workercd.HasValue)
            claims.Add(new Claim("WorkerCd", user.Workercd.Value.ToString(), ClaimValueTypes.String));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(3),
            signingCredentials: creds
        );

        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token)
        });
    }
}