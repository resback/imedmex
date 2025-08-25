using System.Net;
using System.Text.Json;

namespace imedmexTODO.Common;
/// <summary>
/// Clase middleware para la gerstión global de excepciones
/// </summary>
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    public ErrorHandlingMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext ctx)
    {
        try
        {
            await _next(ctx);
        }
        catch (Exception ex)
        {
            ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            ctx.Response.ContentType = "application/json";
            var resp = ApiResponse<object>.Fallo(CodigosError.ErrorServidor, "Ocurrió un error inesperado.", new[] { ex.Message });
            var json = JsonSerializer.Serialize(resp);
            await ctx.Response.WriteAsync(json);
        }
    }
}

