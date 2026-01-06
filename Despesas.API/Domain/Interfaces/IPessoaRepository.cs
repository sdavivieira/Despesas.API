using Despesas.API.Domain.Entities;
using Despesas.API.RequestResponse;

namespace Despesas.API.Domain.Interfaces
{
	public interface IPessoaRepository
	{
		Task<List<Pessoa>> ObterTodas();
		Task<Pessoa?> ObterPorId(int id);
		Task Adicionar(Pessoa pessoa);
		Task Atualizar(Pessoa pessoa);
		Task Remover(Pessoa pessoa);
	}

}
