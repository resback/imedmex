namespace imedmexTODO.Domain.Entities;

public class Usuario
{
    public int Id { get; set; }
    public string NombreUsuario { get; set; } = "";
    public string ContrasenaHash { get; set; } = "";
    public string Rol { get; set; } = "Admin";
}

