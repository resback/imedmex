using imedmexTODO.Domain.Enums;

namespace imedmexTODO.DTOs.Tareas;

public class TareaCrearDto
{
    public string Titulo { get; set; } = "";
    public string? Descripcion { get; set; }
    public DateTime? FechaVencimiento { get; set; }
    public Prioridad Prioridad { get; set; } = Prioridad.Media;
}

