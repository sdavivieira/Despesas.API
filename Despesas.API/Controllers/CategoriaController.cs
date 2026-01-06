using Despesas.API.Application.Interfaces;
using Despesas.API.Application.Services;
using Despesas.API.Domain.Entities;
using Despesas.API.RequestResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Despesas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
		private readonly ICategoriaService _categoriaService;
		public CategoriaController(Application.Interfaces.ICategoriaService categoriaService)
		{
			_categoriaService = categoriaService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var categoria = await _categoriaService.ObterTodasCategorias();
			return Ok(categoria);
		}

		[HttpGet]
		[Route("{id:int}")]
		public async Task<IActionResult> GetById(int id)
		{
			var categoria = await _categoriaService.ObterCategoriaPorId(id);
			if (categoria == null)
			{
				return NotFound();
			}
			return Ok(categoria);
		}

		[HttpGet("totais")]
		public async Task<IActionResult> ConsultaTotaisPorCategoria()
		{
			var totais = await _categoriaService.ConsultaTotaisPorCategoria();
			if (totais == null)
			{
				return NotFound();
			}
			return Ok(totais);

		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CategoriaRequest request)
		{
			var pessoa = new Categoria
			{
				Descricao = request.Descricao,
				FinalidadeCategoria = request.FinalidadeCategoria
			};

			var response = await _categoriaService.RegistrarCategoria(pessoa);

			if (!response.Sucess)
				return BadRequest(response.Message);

			return CreatedAtAction(
				nameof(GetById),
				new { id = pessoa.Id },
				new
				{
					message = response.Message,
					request.Descricao
				}
			);
		}
	}
}
