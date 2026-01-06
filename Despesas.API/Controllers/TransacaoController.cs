using Despesas.API.Application.Interfaces;
using Despesas.API.Domain.Entities;
using Despesas.API.RequestResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Despesas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransacaoController : ControllerBase
    {
		private readonly ITransacaoService _transacaoService;
		public TransacaoController(Application.Interfaces.ITransacaoService transacaoService)
		{
			_transacaoService = transacaoService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var categoria = await _transacaoService.ObterTodasTransacoes();
			return Ok(categoria);
		}

		[HttpGet]
		[Route("{id:int}")]
		public async Task<IActionResult> GetById(int id)
		{
			var categoria = await _transacaoService.ObterTransacaoPorId(id);
			if (categoria == null)
			{
				return NotFound();
			}
			return Ok(categoria);
		}

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
					request.Descricao
				}
			);
		}
	}
}
