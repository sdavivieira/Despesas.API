namespace Despesas.API.Domain.Dtos
{
    public class CategoriaTotaisDto
    {
		public int CategoriaId { get; set; }
		public string Descricao { get; set; } = string.Empty;
		public decimal TotalReceitas { get; set; }
		public decimal TotalDespesas { get; set; }
		public decimal SaldoLiquido { get; set; }
	}
	public class TotaisCategoriaDto
	{
		public decimal TotalReceitas { get; set; }
		public decimal TotalDespesas { get; set; }
		public decimal SaldoLiquido { get; set; }
	}
}
