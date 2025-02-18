using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace L01_2022EO650_2022HC650.Models
{
    public class restauranteContext: DbContext
    {
        public restauranteContext(DbContextOptions<restauranteContext> options) : base(options)
        {

        }

        public DbSet<Clientes> clientes { get; set; }
        public DbSet<Motoristas> motoristas { get; set; }
        public DbSet<Platos> platos { get; set; }
        public DbSet<Pedidos> pedidos { get; set; }
    }
}
