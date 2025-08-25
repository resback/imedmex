using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using imedmexTODO.Common;
using imedmexTODO.DTOs.Tareas;
using imedmexTODO.Services.Interfaces;

namespace imedmexTODO.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize]
public class TareasController : ControllerBase
{
    private readonly ITareaService _svc;

    // Inyección de independecia del servicio
    public TareasController(ITareaService svc) => _svc = svc;

    [HttpGet("listar")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<TareaDto>>), 200)]
    public async Task<ActionResult<ApiResponse<IEnumerable<TareaDto>>>> Listar()
        => Ok(ApiResponse<IEnumerable<TareaDto>>.Ok(await _svc.ListarAsync()));



    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<TareaDto>), 200)]
    [ProducesResponseType(typeof(ApiResponse<TareaDto>), 404)]
    public async Task<ActionResult<ApiResponse<TareaDto>>> Obtener(int id)
    {
        var t = await _svc.ObtenerAsync(id);
        if (t is null)
            return NotFound(ApiResponse<TareaDto>.Fallo(CodigosError.NoEncontrado, "Tarea no encontrada."));
        return Ok(ApiResponse<TareaDto>.Ok(t));
    }



    [HttpPost("crear")]
    [ProducesResponseType(typeof(ApiResponse<TareaDto>), 200)]
    [ProducesResponseType(typeof(ApiResponse<TareaDto>), 400)]
    public async Task<ActionResult<ApiResponse<TareaDto>>> Crear([FromBody] TareaCrearDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Titulo))
            return BadRequest(ApiResponse<TareaDto>.Fallo(CodigosError.Validacion, "El título es obligatorio."));
        var creado = await _svc.CrearAsync(dto);
        return Ok(ApiResponse<TareaDto>.Ok(creado, "Tarea creada."));
    }



    [HttpPut("{id:int}/editar")]
    [ProducesResponseType(typeof(ApiResponse<TareaDto>), 200)]
    [ProducesResponseType(typeof(ApiResponse<TareaDto>), 404)]
    public async Task<ActionResult<ApiResponse<TareaDto>>> Editar(int id, [FromBody] TareaEditarDto dto)
    {
        var editado = await _svc.EditarAsync(id, dto);
        if (editado is null)
            return NotFound(ApiResponse<TareaDto>.Fallo(CodigosError.NoEncontrado, "Tarea no encontrada."));
        return Ok(ApiResponse<TareaDto>.Ok(editado, "Tarea actualizada."));
    }



    [HttpPatch("{id:int}/completar")]
    [ProducesResponseType(typeof(ApiResponse<object>), 200)]
    [ProducesResponseType(typeof(ApiResponse<object>), 404)]
    public async Task<ActionResult<ApiResponse<object>>> Completar(int id)
    {
        var ok = await _svc.CompletarAsync(id);
        if (!ok)
            return NotFound(ApiResponse<object>.Fallo(CodigosError.NoEncontrado, "Tarea no encontrada."));
        return Ok(ApiResponse<object>.Ok(new { Id = id }, "Tarea completada."));
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), 200)]
    [ProducesResponseType(typeof(ApiResponse<object>), 404)]
    public async Task<ActionResult<ApiResponse<object>>> Eliminar(int id)
    {
        var ok = await _svc.EliminarAsync(id);
        if (!ok)
            return NotFound(ApiResponse<object>.Fallo(CodigosError.NoEncontrado, "Tarea no encontrada."));
        return Ok(ApiResponse<object>.Ok(new { Id = id }, "Tarea eliminada."));
    }
}

