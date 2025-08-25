namespace imedmexTODO.DTOs.Auth;

public class JwtTokenResponse
{
    public string Token { get; set; } = "";
    public DateTime Expira { get; set; }
}

