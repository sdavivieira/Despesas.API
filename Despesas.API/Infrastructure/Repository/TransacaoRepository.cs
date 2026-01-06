using Despesas.API.Domain.Entities;
using Despesas.API.Domain.Interfaces;
using Despesas.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Despesas.API.Infrastructure.Repository
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private readonly DespesasDBContext _context;

        public TransacaoRepository(DespesasDBContext context)
        {
            _context = context;
        }

		public async Task<List<Transacao>> ObterTodasTransacoes()
		{
			return await _context.Transacoes
				.Include(t => t.Pessoa)
				.Include(t => t.Categoria)
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<Transacao> ObterTransacaoPorId(int transacaoId)
		{
			return await _context.Transacoes
				.Include(t => t.Pessoa)
				.Include(t => t.Categoria)
				.FirstOrDefaultAsync(t => t.Id == transacaoId);
		}


		public async Task Registrar(Transacao transacao)
        {
            await _context.Transacoes.AddAsync(transacao);
			await _context.SaveChangesAsync();

		}
    }
}
