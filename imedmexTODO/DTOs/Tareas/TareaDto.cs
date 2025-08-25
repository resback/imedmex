using imedmexTODO.Domain.Enums;

namespace imedmexTODO.DTOs.Tareas;

public class TareaDto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = "";
    public string? Descripcion { get; set; }
    public bool Completada { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime? FechaVencimiento { get; set; }
    public Prioridad Prioridad { get; set; }
}

