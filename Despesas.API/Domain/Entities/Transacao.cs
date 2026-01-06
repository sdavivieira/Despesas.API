using static Despesas.API.Domain.Enum.Enums;

namespace Despesas.API.Domain.Entities
{
	public class Transacao
	{
		public int Id { get; set; }
		public string Descricao { get; set; } = string.Empty;
		public decimal Valor { get; set; }
		public TipoTransacao TipoTransacao { get; set; }

		public int PessoaId { get; set; }
		public Pessoa Pessoa { get; set; }
		public int CategoriaId { get; set; }
		public Categoria Categoria { get; set; }

	}
}
