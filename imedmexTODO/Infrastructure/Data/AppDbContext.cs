using Microsoft.EntityFrameworkCore;
using imedmexTODO.Domain.Entities;
using System.Security.Cryptography;
using System.Text;

namespace imedmexTODO.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Tarea> Tareas => Set<Tarea>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed: usuario de prueba (user: prueba, pass: Prueba123*)
        var hash = Sha256("Prueba123*");
        modelBuilder.Entity<Usuario>().HasData(new Usuario
        {
            Id = 1,
            NombreUsuario = "prueba",
            ContrasenaHash = hash,
            Rol = "Admin"
        });
        base.OnModelCreating(modelBuilder);
    }

    public static string Sha256(string input)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
        return BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
    }
}
