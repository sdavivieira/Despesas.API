using Despesas.API.Domain.Entities;
using Despesas.API.RequestResponse;

namespace Despesas.API.Application.Interfaces
{
    public interface ITransacaoService
    {
        Task<DefaultResponse> Registrar(Transacao transacao);
        Task<Transacao>ObterTransacaoPorId(int transacaoId);
        Task<List<Transacao>> ObterTodasTransacoes();   

	}
}
