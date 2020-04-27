using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoceGlamourCore.Models
{
    public class ProdutoPedidoContext : DbContext
    {
        public ProdutoPedidoContext(DbContextOptions<ProdutoPedidoContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProdutoPedidoModel>().HasKey(sc => new { sc.codigo_pedido, sc.codigo_produto });

            modelBuilder.Entity<ProdutoPedidoModel>().HasOne(op => op.PedidoModel)
                                                     .WithMany(op => op.produto_pedido)
                                                     .HasForeignKey(op => op.codigo_pedido)
                                                     .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProdutoPedidoModel>().HasOne(op => op.ProdutoModel)
                                                     .WithMany(op => op.produtoPedidoModels)
                                                     .HasForeignKey(op => op.codigo_produto)
                                                     .OnDelete(DeleteBehavior.Cascade);


        }
        public DbSet<PedidoModel> pedido { get; set; }
        public DbSet<ProdutoPedidoModel> produto_pedido { get; set; }
        public DbSet<ProdutoModel> produto { get; set; }
    }
}







