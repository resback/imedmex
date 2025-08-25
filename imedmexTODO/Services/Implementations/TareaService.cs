
using Microsoft.EntityFrameworkCore;
using imedmexTODO.DTOs.Tareas;
using imedmexTODO.Infrastructure.Data;
using imedmexTODO.Domain.Entities;
using imedmexTODO.Services.Interfaces;

namespace imedmexTODO.Services.Implementations;

public class TareaService : ITareaService
{
    private readonly AppDbContext _db;
    public TareaService(AppDbContext db) => _db = db;

    public async Task<IEnumerable<TareaDto>> ListarAsync()
        => await _db.Tareas
            .OrderByDescending(t => t.Id)
            .Select(t => new TareaDto
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Descripcion = t.Descripcion,
                Completada = t.Completada,
                FechaCreacion = t.FechaCreacion,
                FechaVencimiento = t.FechaVencimiento,
                Prioridad = t.Prioridad
            }).ToListAsync();

    public async Task<TareaDto?> ObtenerAsync(int id)
        => await _db.Tareas.Where(t => t.Id == id)
            .Select(t => new TareaDto
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Descripcion = t.Descripcion,
                Completada = t.Completada,
                FechaCreacion = t.FechaCreacion,
                FechaVencimiento = t.FechaVencimiento,
                Prioridad = t.Prioridad
            }).FirstOrDefaultAsync();

    public async Task<TareaDto> CrearAsync(TareaCrearDto dto)
    {
        var ent = new Tarea
        {
            Titulo = dto.Titulo.Trim(),
            Descripcion = dto.Descripcion,
            FechaVencimiento = dto.FechaVencimiento,
            Prioridad = dto.Prioridad
        };
        _db.Tareas.Add(ent);
        await _db.SaveChangesAsync();
        return await ObtenerAsync(ent.Id) ?? throw new InvalidOperationException("No se pudo crear la tarea.");
    }

    public async Task<TareaDto?> EditarAsync(int id, TareaEditarDto dto)
    {
        var ent = await _db.Tareas.FindAsync(id);
        if (ent is null) return null;
        ent.Titulo = dto.Titulo.Trim();
        ent.Descripcion = dto.Descripcion;
        ent.Completada = dto.Completada;
        ent.FechaVencimiento = dto.FechaVencimiento;
        ent.Prioridad = dto.Prioridad;
        await _db.SaveChangesAsync();
        return await ObtenerAsync(id);
    }

    public async Task<bool> CompletarAsync(int id)
    {
        var ent = await _db.Tareas.FindAsync(id);
        if (ent is null) return false;
        ent.Completada = true;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var ent = await _db.Tareas.FindAsync(id);
        if (ent is null) return false;
        _db.Tareas.Remove(ent);
        await _db.SaveChangesAsync();
        return true;
    }
}
