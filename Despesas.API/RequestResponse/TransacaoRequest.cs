using Despesas.API.Domain.Entities;
using static Despesas.API.Domain.Enum.Enums;

namespace Despesas.API.RequestResponse
{
    public class TransacaoRequest
    {
		public string Descricao { get; set; } = string.Empty;
		public decimal Valor { get; set; }
		public TipoTransacao TipoTransacao { get; set; }
		public int PessoaId { get; set; }
		public int CategoriaId { get; set; }
	}
}
