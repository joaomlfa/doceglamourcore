using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoceGlamourCore.Models
{
    public class PedidoContext : DbContext
    {
        public PedidoContext(DbContextOptions<PedidoContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProdutoPedidoModel>().HasKey(sc => new { sc.codigo_pedido, sc.id_produto });
            
        }
        public DbSet<PedidoModel> pedido { get; set; }
        public DbSet<ClienteModel>cliente{ get; set; }
        public DbSet<ProdutoPedidoModel> produto_pedido { get; set; }
        public DbSet<ProdutoModel>produto { get; set; }
    }
}

