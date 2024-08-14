public class Plataforma
{
    public int Id { get; set; }
    public string? Nombre { get; set; }

    //Relacion uno a muchos: Una plataforma puede tener varias transacciones
    public ICollection<Transaccion>? Transacciones { get; set; }
}