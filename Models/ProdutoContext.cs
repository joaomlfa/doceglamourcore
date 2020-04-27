using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoceGlamourCore.Models
{
    public class ProdutoContext : DbContext
    {
        public ProdutoContext(DbContextOptions<ProdutoContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProdutoPedidoModel>().HasKey(sc => new { sc.codigo_pedido, sc.codigo_produto });

        }

        public DbSet<ProdutoModel> produto { get; set; }
    }
}
