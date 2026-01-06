using Despesas.API.Application.Interfaces;
using Despesas.API.Application.Services;
using Despesas.API.Domain.Interfaces;
using Despesas.API.Infrastructure.Data;
using Despesas.API.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Despesas.API.Infrastructure.IoC
{
	public static class DependencyContainer
	{
		public static void RegisterServices(
			IServiceCollection services,
			IConfiguration configuration)
		{
			// Database
			services.AddDbContext<DespesasDBContext>(options =>
			{
				options.UseSqlite(
					configuration.GetConnectionString("DespesasDbConnection"),
					b => b.MigrationsAssembly(
						typeof(DespesasDBContext).Assembly.FullName
					)
				);
			});

			// Application Services
			services.AddScoped<IPessoaService, PessoaService>();
			services.AddScoped<ICategoriaService, CategoriaService>();
			services.AddScoped<ITransacaoService, TransacaoService>();

			//// Repositories
			services.AddScoped<IPessoaRepository, PessoaRepository>();
			services.AddScoped<ICategoriaRepository, CategoriaRepository>();
			services.AddScoped<ITransacaoRepository, TransacaoRepository>();
		}
	}
}
