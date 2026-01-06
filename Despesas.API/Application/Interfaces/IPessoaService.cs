using Despesas.API.Domain.Entities;
using Despesas.API.RequestResponse;

namespace Despesas.API.Application.Interfaces
{
    public interface IPessoaService
    {
        public Task<List<Pessoa>>ObterTodasPessoas();
        public Task<Pessoa>ObterPessoaPorId(int pessoaId);
        public Task<DefaultResponse>RegistrarPessoa(Pessoa pessoa);
        public Task<DefaultResponse>AtualizarPessoa(Pessoa pessoa);
        public Task<DefaultResponse>ExcluirPessoa(int pessoaId);
        Task<PessoaTotaisResponse> ConsultaTotaisPorPessoa();
    }
}
