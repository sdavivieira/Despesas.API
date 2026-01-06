using Despesas.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Despesas.API.Infrastructure.Data
{
	/// <summary>
	/// DbContext responsável pelo acesso ao banco de dados da aplicação.
	/// Contém o mapeamento das entidades e regras de relacionamento.
	/// </summary>
	public class DespesasDBContext : DbContext
	{
		public DespesasDBContext(DbContextOptions<DespesasDBContext> options)
			: base(options)
		{
		}

		public DbSet<Pessoa> Pessoas => Set<Pessoa>();
		public DbSet<Categoria> Categorias => Set<Categoria>();
		public DbSet<Transacao> Transacoes => Set<Transacao>();

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Pessoa>(entity =>
			{
				entity.HasKey(e => e.Id);

				entity.Property(e => e.Nome)
					  .IsRequired()
					  .HasMaxLength(50);

				entity.Property(e => e.Idade)
					  .IsRequired();
			});

			modelBuilder.Entity<Categoria>(entity =>
			{
				entity.HasKey(e => e.Id);

				entity.Property(e => e.Descricao)
					  .IsRequired()
					  .HasMaxLength(50);

				entity.Property(e => e.FinalidadeCategoria)
					  .IsRequired();
			});

			modelBuilder.Entity<Transacao>(entity =>
			{
				entity.HasKey(e => e.Id);

				entity.Property(e => e.Descricao)
					  .IsRequired()
					  .HasMaxLength(100);

				entity.Property(e => e.Valor)
					  .HasPrecision(18, 2)
					  .IsRequired();

				entity.Property(e => e.TipoTransacao)
					  .IsRequired();

				entity.HasOne(t => t.Pessoa)
					  .WithMany(p => p.Transacoes)
					  .HasForeignKey(t => t.PessoaId)
					  .OnDelete(DeleteBehavior.Cascade); // Quando uma Pessoa for deletada, suas Transações também serão deletadas.

				entity.HasOne(t => t.Categoria)
					  .WithMany()
					  .HasForeignKey(t => t.CategoriaId);
			});

			base.OnModelCreating(modelBuilder);
		}
	}
}
