using Despesas.API.Application.Interfaces;
using Despesas.API.Domain.Entities;
using Despesas.API.RequestResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Despesas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaService _pessoaService;
        public PessoaController(Application.Interfaces.IPessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pessoas = await _pessoaService.ObterTodasPessoas();
            return Ok(pessoas);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pessoa = await _pessoaService.ObterPessoaPorId(id);
            if (pessoa == null)
            {
                return NotFound();
            }
            return Ok(pessoa);
        }


        [HttpGet("Totais")]
		public async Task<IActionResult> ConsultaTotaisPorPessoa()
        {
            var pessoasComDespesas = await _pessoaService.ConsultaTotaisPorPessoa();

			if (pessoasComDespesas == null)
			{
				return NotFound();
			}
			return Ok(pessoasComDespesas);
		}

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


		[HttpPut]
        public async Task<IActionResult> Update([FromBody] Pessoa pessoa)
        {
            var response = await _pessoaService.AtualizarPessoa(pessoa);
            if (!response.Sucess)
            {
                return NotFound(response.Message);
            }
			return Ok(new
			{
				message = response.Message
			});

		}

		[HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _pessoaService.ExcluirPessoa(id);
            if (!response.Sucess)
            {
                return NotFound(response.Message);
            }
			return Ok(new
			{
				message = response.Message
			});

		}
	}
}
