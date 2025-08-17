using CoreData.Models.Clientes;
using CoreData.Models.Produtos;
using CoreData.Models.Usuario;
using Microsoft.EntityFrameworkCore;

namespace CoreData.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; } 

        
    }
}