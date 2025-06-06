using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using VKR2.DAL.Models;
using VKR2.DAL;

namespace VKR2.Domain.Services;

public class AuthService
{
    private readonly IConfiguration _configuration;
    private readonly PostgresContext _context;

    public AuthService(IConfiguration configuration, PostgresContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    public async Task<string> GenerateJwtToken(User user)
    {
        var roles = await _context.Rolesusers
            .Where(ru => ru.Usercd == user.Usercd)
            .Include(ru => ru.Role)
            .Select(ru => ru.Role.Rolename)
            .ToListAsync();

        var primaryRole = roles.FirstOrDefault() ?? "User";

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email, ClaimValueTypes.String),
            new Claim("userId", user.Usercd.ToString(), ClaimValueTypes.String),
            new Claim(ClaimTypes.Role, primaryRole, ClaimValueTypes.String)
        };

        if (user.Parentcd != null)
            claims.Add(new Claim("ParentCd", user.Parentcd.ToString(), ClaimValueTypes.String));

        if (user.Workercd != null)
            claims.Add(new Claim("WorkerCd", user.Workercd.ToString(), ClaimValueTypes.String));

        foreach (var role in roles)
        {
            claims.Add(new Claim("roles", role, ClaimValueTypes.String));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}