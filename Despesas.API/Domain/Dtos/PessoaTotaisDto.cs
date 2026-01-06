namespace Despesas.API.Domain.Dtos
{
    public class PessoaTotaisDto
    {
		public int PessoaId { get; set; }
		public string Nome { get; set; } = string.Empty;
		public decimal TotalReceitas { get; set; }
		public decimal TotalDespesas { get; set; }
		public decimal Saldo { get; set; }
	}
    public class TotaisDto
    {
		public decimal TotalReceitas { get; set; }
		public decimal TotalDespesas { get; set; }
		public decimal Saldo { get; set; }
	}
}
