using DoceGlamourCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoceGlamourCore.Context
{
    public class CaixaContext : DbContext
    {
        public CaixaContext(DbContextOptions<CaixaContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
        public DbSet<CaixaModel> caixa { get; set; }
    }
}
