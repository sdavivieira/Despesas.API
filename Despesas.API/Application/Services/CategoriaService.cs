using Despesas.API.Application.Interfaces;
using Despesas.API.Domain.Dtos;
using Despesas.API.Domain.Entities;
using Despesas.API.Domain.Enum;
using Despesas.API.Domain.Interfaces;
using Despesas.API.RequestResponse;
using static Despesas.API.Domain.Enum.Enums;

namespace Despesas.API.Application.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ITransacaoRepository _transacaoRepository;

		public CategoriaService(ICategoriaRepository categoriaRepository, ITransacaoRepository transacaoRepository)
        {
            _categoriaRepository = categoriaRepository;
			_transacaoRepository = transacaoRepository;
		}


        public async Task<Categoria> ObterCategoriaPorId(int categoriaId)
        {
            return await _categoriaRepository.ObterPorId(categoriaId);
		}

        public async Task<List<Categoria>> ObterTodasCategorias()
        {
			return await _categoriaRepository.ObterTodasCategorias();
		}

        public async Task<DefaultResponse> RegistrarCategoria(Categoria categoria)
        {
			if (!Enum.IsDefined(typeof(Enums.FinalidadeCategoria), categoria.FinalidadeCategoria))
			{
				return new DefaultResponse
				{
					Sucess = false,
					Message = "Finalidade da categoria inválida."
				};
			}

			if (string.IsNullOrWhiteSpace(categoria.Descricao))
			{
				return new DefaultResponse
				{
					Sucess = false,
					Message = "A descrição da categoria não pode ser vazia."
				};
			}


			await _categoriaRepository.Adicionar(categoria);
			return new DefaultResponse
			{
				Sucess = true,
				Message = "Categoria registrada com sucesso."
			};
		}
        public async Task<CategoriaTotaisResponse> ConsultaTotaisPorCategoria()
        {
            var categorias = await _categoriaRepository.ObterTodasCategorias();
            var transacoes = await _transacaoRepository.ObterTodasTransacoes();
			if (categorias == null)
            {
                return null;
            }

            var response = new CategoriaTotaisResponse
            {
                Itens = new List<CategoriaTotaisDto>(),
                TotaisGerais = new TotaisCategoriaDto()

            };
            foreach(var categoria in categorias)
            {
                var transacoesCategoria = transacoes.Where(t => t.CategoriaId == categoria.Id).ToList();

				var totalReceitas = transacoesCategoria.Where(t => t.TipoTransacao == TipoTransacao.Receita)
                .Sum(t => t.Valor);
                var totalDespesar = transacoesCategoria.Where(t => t.TipoTransacao == TipoTransacao.Despesa)
                 .Sum(t => t.Valor);

				var saldo = totalReceitas - totalDespesar;

                response.Itens.Add(new CategoriaTotaisDto
                {
                    CategoriaId = categoria.Id,
                    Descricao = categoria.Descricao,
                    TotalReceitas = totalReceitas,
                    TotalDespesas = totalDespesar,
                    SaldoLiquido = saldo
                });

                response.TotaisGerais.TotalReceitas += totalReceitas;
                response.TotaisGerais.TotalDespesas += totalDespesar;
                response.TotaisGerais.SaldoLiquido += saldo;
			}
            return response;
		}
    }
}
