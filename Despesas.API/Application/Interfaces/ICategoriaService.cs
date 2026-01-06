using Despesas.API.Domain.Entities;
using Despesas.API.RequestResponse;

namespace Despesas.API.Application.Interfaces
{
    public interface ICategoriaService
    {
	    Task<List<Categoria>> ObterTodasCategorias();
		Task<Categoria> ObterCategoriaPorId(int categoriaId);
		Task<DefaultResponse> RegistrarCategoria(Categoria categoria);
        Task <CategoriaTotaisResponse> ConsultaTotaisPorCategoria();
    }
}
