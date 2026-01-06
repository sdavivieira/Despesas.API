
using Despesas.API.Domain.Entities;

namespace Despesas.API.Domain.Interfaces
{
    public interface ICategoriaRepository
    {
        Task Adicionar(Categoria categoria);
        Task<Categoria>ObterPorId(int categoriaId);
        Task<List<Categoria>> ObterTodasCategorias();
    }
}
