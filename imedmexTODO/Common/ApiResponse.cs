namespace imedmexTODO.Common;
/// <summary>
/// Clase para estándarizar las respuestas en el front
/// </summary>
/// <typeparam name="T"></typeparam>
public class ApiResponse<T>
{
    public bool Exito { get; set; }
    public int Codigo { get; set; }
    public string Mensaje { get; set; } = "";
    public T? Datos { get; set; }
    public IEnumerable<string>? Errores { get; set; }

    public static ApiResponse<T> Ok(T datos, string mensaje = "OK")
        => new() { Exito = true, Codigo = CodigosError.Ninguno, Mensaje = mensaje, Datos = datos };

    public static ApiResponse<T> Fallo(int codigo, string mensaje, IEnumerable<string>? errores = null)
        => new() { Exito = false, Codigo = codigo, Mensaje = mensaje, Errores = errores };
}

