using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoceGlamourCore.Models
{
    public class ClienteContext : DbContext
    {
        public ClienteContext(DbContextOptions<ClienteContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProdutoPedidoModel>().HasKey(sc => new { sc.codigo_pedido, sc.codigo_produto });

        }
        public DbSet<ClienteModel> cliente { get; set; }
    }
}
