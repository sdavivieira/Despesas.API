using Despesas.API.Domain.Entities;
using Despesas.API.Domain.Interfaces;
using Despesas.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Despesas.API.Infrastructure.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly DespesasDBContext _context;

        public CategoriaRepository(DespesasDBContext context)
        {
            _context = context;
		}
        public async Task Adicionar(Categoria categoria)
        {
			await _context.Categorias.AddAsync(categoria);
			await _context.SaveChangesAsync();
		}

        public async Task<Categoria> ObterPorId(int categoriaId)
        {
            return await _context.Categorias.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == categoriaId);
		}

        public async Task<List<Categoria>> ObterTodasCategorias()
        {
            return await _context.Categorias.AsNoTracking().ToListAsync();
		}
    }
}
