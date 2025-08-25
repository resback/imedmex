using Microsoft.AspNetCore.Mvc;
using imedmexTODO.Common;
using imedmexTODO.DTOs.Auth;
using imedmexTODO.Services.Interfaces;

namespace imedmexTODO.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly IJwtService _jwt;
    
    // Inyección de independecia del servicio en el constructor

    public AuthController(IJwtService jwt) => _jwt = jwt;

    [HttpPost("login")]
    [ProducesResponseType(typeof(ApiResponse<JwtTokenResponse>), 200)]
    public async Task<ActionResult<ApiResponse<JwtTokenResponse>>> Login([FromBody] LoginRequest req)
    {
        if (string.IsNullOrWhiteSpace(req.Usuario) || string.IsNullOrWhiteSpace(req.Contrasena))
            return BadRequest(ApiResponse<JwtTokenResponse>.Fallo(CodigosError.PeticionInvalida, "Usuario y contraseña son requeridos."));

        var token = await _jwt.AutenticarAsync(req.Usuario, req.Contrasena);
        if (token is null)
            return Unauthorized(ApiResponse<JwtTokenResponse>.Fallo(CodigosError.NoAutorizado, "Credenciales inválidas."));

        return Ok(ApiResponse<JwtTokenResponse>.Ok(token, "Autenticación exitosa"));
    }
}

