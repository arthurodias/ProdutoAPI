using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Infrastructure.Data
{
    public class ProdutoDbContext(DbContextOptions<ProdutoDbContext> options) : DbContext(options)
    {
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Descricao).IsRequired();
                entity.Property(e => e.Situacao).IsRequired();
                entity.Property(e => e.DataFabricacao);
                entity.Property(e => e.DataValidade);
                entity.Property(e => e.CodigoFornecedor);
                entity.Property(e => e.DescricaoFornecedor);
                entity.Property(e => e.CnpjFornecedor);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}