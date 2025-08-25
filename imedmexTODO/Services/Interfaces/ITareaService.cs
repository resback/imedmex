
using imedmexTODO.DTOs.Tareas;

namespace imedmexTODO.Services.Interfaces;

public interface ITareaService
{
    Task<IEnumerable<TareaDto>> ListarAsync();
    Task<TareaDto?> ObtenerAsync(int id);
    Task<TareaDto> CrearAsync(TareaCrearDto dto);
    Task<TareaDto?> EditarAsync(int id, TareaEditarDto dto);
    Task<bool> CompletarAsync(int id);
    Task<bool> EliminarAsync(int id);
}
