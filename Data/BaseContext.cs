using Microsoft.EntityFrameworkCore;
using PruebaCelsia.Models;

namespace PruebaCelsia.Data
{
    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions<BaseContext> options) : base (options){

        }

        public DbSet<Cliente> Clientes {get ; set; }
        public DbSet<Factura> Facturas {get ; set; }
        public DbSet<Transaccion> Transacciones {get ; set; }
        public DbSet<Plataforma> Plataformas {get ; set; }

        
    }
}