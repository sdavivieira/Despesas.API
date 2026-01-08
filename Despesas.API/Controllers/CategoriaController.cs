using Despesas.API.Application.Interfaces;
using Despesas.API.Domain.Entities;
using Despesas.API.RequestResponse;
using Microsoft.AspNetCore.Mvc;

namespace Despesas.API.Controllers
{
	/// <summary>
	/// Controller responsável pelo gerenciamento de categorias.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriaController : ControllerBase
	{
		private readonly ICategoriaService _categoriaService;

		/// <summary>
		/// Construtor do controller de categoria.
		/// </summary>
		/// <param name="categoriaService">Serviço de domínio para categorias</param>
		public CategoriaController(ICategoriaService categoriaService)
		{
			_categoriaService = categoriaService;
		}

		/// <summary>
		/// Retorna todas as categorias cadastradas.
		/// </summary>
		/// <returns>Lista de categorias</returns>
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var categorias = await _categoriaService.ObterTodasCategorias();
			return Ok(categorias);
		}

		/// <summary>
		/// Retorna uma categoria pelo seu identificador.
		/// </summary>
		/// <param name="id">Identificador da categoria</param>
		/// <returns>Categoria correspondente ou 404</returns>
		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetById(int id)
		{
			var categoria = await _categoriaService.ObterCategoriaPorId(id);

			if (categoria == null)
				return NotFound();

			return Ok(categoria);
		}

		/// <summary>
		/// Consulta o total de valores agrupados por categoria.
		/// </summary>
		/// <returns>Totais por categoria</returns>
		[HttpGet("totais")]
		public async Task<IActionResult> ConsultaTotaisPorCategoria()
		{
			var totais = await _categoriaService.ConsultaTotaisPorCategoria();

			if (totais == null)
				return NotFound();

			return Ok(totais);
		}

		/// <summary>
		/// Cadastra uma nova categoria.
		/// </summary>
		/// <param name="request">Dados da categoria</param>
		/// <returns>Categoria criada</returns>
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CategoriaRequest request)
		{
			var categoria = new Categoria
			{
				Descricao = request.Descricao,
				FinalidadeCategoria = request.FinalidadeCategoria
			};

			var response = await _categoriaService.RegistrarCategoria(categoria);

			if (!response.Sucess)
				return BadRequest(response.Message);

			return CreatedAtAction(
				nameof(GetById),
				new { id = categoria.Id },
				new
				{
					message = response.Message,
					categoria.Id,
					categoria.Descricao
				}
			);
		}
	}
}
