using Despesas.API.Domain.Entities;
using Despesas.API.Domain.Interfaces;
using Despesas.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Despesas.API.Infrastructure.Repository
{

    public class PessoaRepository : IPessoaRepository
    {
        private readonly DespesasDBContext _context;

        public PessoaRepository(DespesasDBContext context)
        {
            _context = context;
        }

		public async Task<List<Pessoa>> ObterTodas()
		{
			return await _context.Pessoas
				.Include(p => p.Transacoes) 
				.AsNoTracking()
				.ToListAsync();
		}


		public async Task<Pessoa?> ObterPorId(int id)
		{
			return await _context.Pessoas
				.AsNoTracking()
				.FirstOrDefaultAsync(p => p.Id == id);
		}

		public async Task Adicionar(Pessoa pessoa)
        {
            await _context.Pessoas.AddAsync(pessoa);
            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(Pessoa pessoa)
        {
            _context.Pessoas.Update(pessoa);
            await _context.SaveChangesAsync();
        }

		public async Task Remover(Pessoa pessoa)
		{
			_context.Pessoas.Remove(pessoa);
			await _context.SaveChangesAsync();
		}

	}
}
