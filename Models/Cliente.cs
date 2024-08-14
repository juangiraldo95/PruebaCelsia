
public class Cliente
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? NumeroIdentificacion { get; set; }
    public string? Direccion { get; set; }
    public string? Telefono { get; set; }
    public string? CorreoElectronico { get; set; }
    
    // Relación uno a muchos: Un cliente puede tener varias transacciones
    public ICollection<Transaccion>? Transacciones { get; set; }
}
