using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using imedmexTODO.Common;
using imedmexTODO.DTOs.Auth;
using imedmexTODO.Infrastructure.Data;
using imedmexTODO.Services.Interfaces;

namespace imedmexTODO.Services.Implementations;

public class JwtService : IJwtService
{
    private readonly AppDbContext _db;
    private readonly JwtSettings _settings;
    private readonly SymmetricSecurityKey _key;

    public JwtService(AppDbContext db, IOptions<JwtSettings> opt)
    {
        _db = db;
        _settings = opt.Value;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
    }

    public async Task<JwtTokenResponse?> AutenticarAsync(string usuario, string contrasena)
    {
        var hash = AppDbContext.Sha256(contrasena);
        var u = await _db.Usuarios.FirstOrDefaultAsync(x =>
                 x.NombreUsuario == usuario && x.ContrasenaHash == hash);

        if (u is null) return null;

        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, u.NombreUsuario),
            new Claim(ClaimTypes.Name, u.NombreUsuario),
            new Claim(ClaimTypes.Role, u.Rol)
        };

        var exp = DateTime.UtcNow.AddMinutes(_settings.ExpiresMinutes);
        var token = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: exp,
            signingCredentials: creds);

        return new JwtTokenResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expira = exp
        };
    }
}
