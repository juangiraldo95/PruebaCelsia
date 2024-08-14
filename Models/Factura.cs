
public class Factura
{
    public int Id { get; set; } 
    public string? NumeroFactura { get; set; }
    public string? PeriodoFacturacion { get; set; }
    public decimal MontoFacturado { get; set; }
    public decimal MontoPagado { get; set; }
    
    // Llave foránea para relacionar con la transacción
    public int TransaccionId { get; set; }
    public Transaccion? Transaccion { get; set; }
}
