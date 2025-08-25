using imedmexTODO.DTOs.Auth;

namespace imedmexTODO.Services.Interfaces;

public interface IJwtService
{
    Task<JwtTokenResponse?> AutenticarAsync(string usuario, string contrasena);
}
