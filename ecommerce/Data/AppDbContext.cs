using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ecommerce.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace ecommerce.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<ClienteModel>(entity =>
            {
                entity.OwnsOne(c => c.Endereco);
            });
            
            modelBuilder.Entity<ItemPedidoModel>()
                .HasKey(e => new { e.IdPedido, e.IdProduto});

                        //restringe a exclusão de clientes que possuem pedidos
            modelBuilder.Entity<PedidoModel>()
                .HasOne<ClienteModel>(p => p.Cliente)
                .WithMany(c => c.Pedidos)
                .HasForeignKey(p => p.IdCliente)
                .OnDelete(DeleteBehavior.Restrict);

            //exclui automaticamento os itens de um pedido quando um pedido é excluído
            modelBuilder.Entity<ItemPedidoModel>()
                .HasOne<PedidoModel>(ip => ip.Pedido)
                .WithMany(p => p.ItensPedido)
                .HasForeignKey(p => p.IdPedido)
                .OnDelete(DeleteBehavior.Cascade);

            //restringe exclusão de produtos que possuem itens pedidos
            modelBuilder.Entity<ItemPedidoModel>()
                .HasOne<ProdutoModel>(ip => ip.Produto)
                .WithMany()
                .HasForeignKey(p => p.IdProduto)
                .OnDelete(DeleteBehavior.Restrict);

        }

        public DbSet<ProdutoModel>? Produto {get; set;}
        public DbSet<ClienteModel>? Cliente {get; set;}
        public DbSet<PedidoModel>? Pedido {get; set;}
        public DbSet<ItemPedidoModel>? ItemPedido {get; set;}

        static readonly string connectionString = "Server=db_mysql;Port=3306;Database=ecommerce;Uid=root;Pwd=ecommerce;";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            optionsBuilder.UseMySql(connectionString,
                                    ServerVersion.AutoDetect(connectionString));
        }
    }
}