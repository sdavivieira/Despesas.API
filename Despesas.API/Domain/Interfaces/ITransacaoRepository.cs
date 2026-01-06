using Despesas.API.Domain.Entities;

namespace Despesas.API.Domain.Interfaces
{
    public interface ITransacaoRepository
    {
        Task<List<Transacao>> ObterTodasTransacoes();
        Task<Transacao> ObterTransacaoPorId(int transacaoId);
        Task Registrar(Transacao transacao);
    }
}
