using Despesas.API.Application.Interfaces;
using Despesas.API.Domain.Entities;
using Despesas.API.RequestResponse;
using Microsoft.AspNetCore.Mvc;

namespace Despesas.API.Controllers
{
	/// <summary>
	/// Controller responsável pelo gerenciamento de transações financeiras.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class TransacaoController : ControllerBase
	{
		private readonly ITransacaoService _transacaoService;

		/// <summary>
		/// Construtor do controller de transações.
		/// </summary>
		/// <param name="transacaoService">Serviço de domínio para transações</param>
		public TransacaoController(ITransacaoService transacaoService)
		{
			_transacaoService = transacaoService;
		}

		/// <summary>
		/// Retorna todas as transações cadastradas.
		/// </summary>
		/// <returns>Lista de transações</returns>
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var transacoes = await _transacaoService.ObterTodasTransacoes();
			return Ok(transacoes);
		}

		/// <summary>
		/// Retorna uma transação pelo seu identificador.
		/// </summary>
		/// <param name="id">Identificador da transação</param>
		/// <returns>Transação correspondente ou 404</returns>
		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetById(int id)
		{
			var transacao = await _transacaoService.ObterTransacaoPorId(id);

			if (transacao == null)
				return NotFound();

			return Ok(transacao);
		}

		/// <summary>
		/// Registra uma nova transação financeira.
		/// </summary>
		/// <param name="request">Dados da transação</param>
		/// <returns>Transação criada</returns>
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] TransacaoRequest request)
		{
			var transacao = new Transacao
			{
				Descricao = request.Descricao,
				Valor = request.Valor,
				TipoTransacao = request.TipoTransacao,
				PessoaId = request.PessoaId,
				CategoriaId = request.CategoriaId
			};

			var response = await _transacaoService.Registrar(transacao);

			if (!response.Sucess)
				return BadRequest(response.Message);

			return CreatedAtAction(
				nameof(GetById),
				new { id = transacao.Id },
				new
				{
					message = response.Message,
					transacao.Descricao
				}
			);
		}
	}
}
