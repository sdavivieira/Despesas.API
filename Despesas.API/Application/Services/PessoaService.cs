using Despesas.API.Application.Interfaces;
using Despesas.API.Domain.Dtos;
using Despesas.API.Domain.Entities;
using Despesas.API.Domain.Interfaces;
using Despesas.API.RequestResponse;
using static Despesas.API.Domain.Enum.Enums;

namespace Despesas.API.Application.Services
{
    public class PessoaService : IPessoaService
    {
        private readonly IPessoaRepository _pessoaRepository;
        public PessoaService(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
		}

        public async Task<List<Pessoa>> ObterTodasPessoas()
        {
           return await _pessoaRepository.ObterTodas();
		}

        public async Task<Pessoa> ObterPessoaPorId(int pessoaId)
        {
            return await _pessoaRepository.ObterPorId(pessoaId);
		}
		public async Task<DefaultResponse> RegistrarPessoa(Pessoa pessoa)
		{
			await _pessoaRepository.Adicionar(pessoa);

			return new DefaultResponse
			{
				Sucess = true,
				Message = "Pessoa registrada com sucesso."
			};
		}


		public async Task<DefaultResponse> AtualizarPessoa(Pessoa pessoa)
		{
			var existente = await _pessoaRepository.ObterPorId(pessoa.Id);

			if (existente == null)
			{
				return new DefaultResponse
				{
					Sucess = false,
					Message = "Pessoa não encontrada."
				};
			}

			if (string.IsNullOrWhiteSpace(pessoa.Nome))
			{
				return new DefaultResponse
				{
					Sucess = false,
					Message = "O nome da pessoa não pode ser vazio."
				};
			}

			if (pessoa.Idade <= 0)
			{
				return new DefaultResponse
				{
					Sucess = false,
					Message = "A idade da pessoa deve ser maior que zero."
				};
			}

			await _pessoaRepository.Atualizar(pessoa);

			return new DefaultResponse
			{
				Sucess = true,
				Message = "Pessoa atualizada com sucesso."
			};
		}


		public async Task<DefaultResponse> ExcluirPessoa(int pessoaId)
		{
			var pessoa = await _pessoaRepository.ObterPorId(pessoaId);
			if (pessoa == null)
			{
				return new DefaultResponse
				{
					Sucess = false,
					Message = "Pessoa não encontrada."
				};
			}

			await _pessoaRepository.Remover(pessoa);

			return new DefaultResponse
			{
				Sucess = true,
				Message = "Pessoa removida com sucesso."
			};
		}

        public async Task<PessoaTotaisResponse> ConsultaTotaisPorPessoa()
        {
           var pessoas = await _pessoaRepository.ObterTodas();
			if (pessoas == null)
			{
				return null;
			}
			var response = new PessoaTotaisResponse
			{
				Itens = new List<PessoaTotaisDto>(),
				TotaisGerais = new TotaisDto()
			};

			foreach(var pessoa in pessoas)
			{			
				var totalReceitas = pessoa.Transacoes
					.Where(t => t.TipoTransacao == TipoTransacao.Receita)
					.Sum(t => t.Valor);
				
				var totalDespesas = pessoa.Transacoes
					.Where(t => t.TipoTransacao == TipoTransacao.Despesa)
					.Sum(t => t.Valor);

				var saldo = totalReceitas - totalDespesas;

				response.Itens.Add(new PessoaTotaisDto
				{
					PessoaId = pessoa.Id,
					Nome = pessoa.Nome,
					TotalReceitas = totalReceitas,
					TotalDespesas = totalDespesas,
					Saldo = saldo
				});

				response.TotaisGerais.TotalReceitas += totalReceitas;
				response.TotaisGerais.TotalDespesas += totalDespesas;
				response.TotaisGerais.Saldo = response.TotaisGerais.TotalReceitas - response.TotaisGerais.TotalDespesas;
			}
			return response;
		}
    }
}
