using Despesas.API.Application.Interfaces;
using Despesas.API.Domain.Entities;
using Despesas.API.RequestResponse;
using Microsoft.AspNetCore.Mvc;

namespace Despesas.API.Controllers
{
	/// <summary>
	/// Controller responsável pelo gerenciamento de pessoas.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class PessoaController : ControllerBase
	{
		private readonly IPessoaService _pessoaService;

		/// <summary>
		/// Construtor do controller de pessoa.
		/// </summary>
		/// <param name="pessoaService">Serviço de domínio para pessoas</param>
		public PessoaController(IPessoaService pessoaService)
		{
			_pessoaService = pessoaService;
		}

		/// <summary>
		/// Retorna todas as pessoas cadastradas.
		/// </summary>
		/// <returns>Lista de pessoas</returns>
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var pessoas = await _pessoaService.ObterTodasPessoas();
			return Ok(pessoas);
		}

		/// <summary>
		/// Retorna uma pessoa pelo seu identificador.
		/// </summary>
		/// <param name="id">Identificador da pessoa</param>
		/// <returns>Pessoa correspondente ou 404</returns>
		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetById(int id)
		{
			var pessoa = await _pessoaService.ObterPessoaPorId(id);

			if (pessoa == null)
				return NotFound();

			return Ok(pessoa);
		}

		/// <summary>
		/// Consulta o total de despesas agrupadas por pessoa.
		/// </summary>
		/// <remarks>
		/// Endpoint utilizado para dashboards e relatórios financeiros.
		/// </remarks>
		/// <returns>Totais financeiros por pessoa</returns>
		[HttpGet("totais")]
		public async Task<IActionResult> ConsultaTotaisPorPessoa()
		{
			var pessoasComDespesas = await _pessoaService.ConsultaTotaisPorPessoa();

			if (pessoasComDespesas == null)
				return NotFound();

			return Ok(pessoasComDespesas);
		}

		/// <summary>
		/// Cadastra uma nova pessoa.
		/// </summary>
		/// <param name="request">Dados da pessoa</param>
		/// <returns>Pessoa criada</returns>
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] PessoaRequest request)
		{
			var pessoa = new Pessoa
			{
				Nome = request.Nome,
				Idade = request.Idade
			};

			var response = await _pessoaService.RegistrarPessoa(pessoa);

			if (!response.Sucess)
				return BadRequest(response.Message);

			return CreatedAtAction(
				nameof(GetById),
				new { id = pessoa.Id },
				new
				{
					message = response.Message,
					pessoa.Nome
				}
			);
		}

		/// <summary>
		/// Atualiza os dados de uma pessoa existente.
		/// </summary>
		/// <param name="pessoa">Dados atualizados da pessoa</param>
		/// <returns>Resultado da atualização</returns>
		[HttpPut]
		public async Task<IActionResult> Update([FromBody] Pessoa pessoa)
		{
			var response = await _pessoaService.AtualizarPessoa(pessoa);

			if (!response.Sucess)
				return NotFound(response.Message);

			return Ok(new
			{
				message = response.Message
			});
		}

		/// <summary>
		/// Remove uma pessoa pelo seu identificador.
		/// </summary>
		/// <param name="id">Identificador da pessoa</param>
		/// <returns>Resultado da exclusão</returns>
		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete(int id)
		{
			var response = await _pessoaService.ExcluirPessoa(id);

			if (!response.Sucess)
				return NotFound(response.Message);

			return Ok(new
			{
				message = response.Message
			});
		}
	}
}
