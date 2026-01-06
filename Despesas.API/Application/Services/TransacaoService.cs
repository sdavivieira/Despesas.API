using Despesas.API.Application.Interfaces;
using Despesas.API.Domain.Entities;
using Despesas.API.Domain.Enum;
using Despesas.API.Domain.Interfaces;
using Despesas.API.Infrastructure.Repository;
using Despesas.API.RequestResponse;
using static Despesas.API.Domain.Enum.Enums;

namespace Despesas.API.Application.Services
{
    public class TransacaoService : ITransacaoService
    {
        private readonly ITransacaoRepository _transacaoRepository;
		private readonly ICategoriaRepository _categoriaRepository;
		private readonly IPessoaRepository _pessoaRepository;
		public TransacaoService(ITransacaoRepository transacaoRepository, ICategoriaRepository categoriaRepository, IPessoaRepository pessoaRepository)
        {
            _transacaoRepository = transacaoRepository;
			_categoriaRepository = categoriaRepository;
			_pessoaRepository = pessoaRepository;
		}
        public async Task<List<Transacao>> ObterTodasTransacoes()
        {
            return await _transacaoRepository.ObterTodasTransacoes();

		}

        public Task<Transacao> ObterTransacaoPorId(int transacaoId)
        {
            return _transacaoRepository.ObterTransacaoPorId(transacaoId);
		}

		public async Task<DefaultResponse> Registrar(Transacao transacao)
		{

			if (!Enum.IsDefined(typeof(Enums.TipoTransacao), transacao.TipoTransacao))
			{
				return new DefaultResponse
				{
					Sucess = false,
					Message = "Tipo de transação inválido."
				};
			}

			if (string.IsNullOrWhiteSpace(transacao.Descricao))
			{
				return new DefaultResponse
				{
					Sucess = false,
					Message = "A descrição da transação não pode ser vazia."
				};
			}

			if (transacao.Valor < 0)
			{
				return new DefaultResponse
				{
					Sucess = false,
					Message = "O valor não pode ser vazio."
				};
			}

			var categoria = await _categoriaRepository.ObterPorId(transacao.CategoriaId);
			var pessoa = await _pessoaRepository.ObterPorId(transacao.PessoaId);

			if (categoria == null)
			{
				return new DefaultResponse
				{
					Sucess = false,
					Message = "Categoria não encontrada."
				};
			}

			if (pessoa == null)
			{
				return new DefaultResponse
				{
					Sucess = false,
					Message = "Pessoa não encontrada."
				};
			}

			if (transacao.TipoTransacao == TipoTransacao.Despesa && categoria.FinalidadeCategoria == FinalidadeCategoria.Receita)
				return new DefaultResponse { Sucess = false, Message = "Não é possível usar categoria de receita em transação de despesa." };

			if (transacao.TipoTransacao == TipoTransacao.Receita && categoria.FinalidadeCategoria == FinalidadeCategoria.Despesa)
				return new DefaultResponse { Sucess = false, Message = "Não é possível usar categoria de despesa em transação de receita." };

			if (pessoa.Idade < 18 && transacao.TipoTransacao == TipoTransacao.Receita)
				return new DefaultResponse { Sucess = false, Message = "Pessoa menor de idade não pode registrar receita." };

			await _transacaoRepository.Registrar(transacao);

			return new DefaultResponse
			{
				Sucess = true,
				Message = "Transação registrada com sucesso."
			};
		}

	}
}
