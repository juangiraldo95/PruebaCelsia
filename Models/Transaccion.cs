
public class Transaccion
{
    public int Id { get; set; } 
    public DateTime FechaHora { get; set; }
    public decimal Monto { get; set; }
    public string? Estado { get; set; }
    public string? Tipo { get; set; }
    
    // Llave foránea para relacionar con el cliente
    public int ClienteId { get; set; }
    public Cliente? Cliente { get; set; }

    // Llave foránea para relacionar con la plataforma
    public int PlataformaId { get; set; }
    public Plataforma? Plataforma { get; set; }
    
    // Relación uno a uno: Una transacción puede estar asociada con una factura
    public Factura? Factura { get; set; }
}
