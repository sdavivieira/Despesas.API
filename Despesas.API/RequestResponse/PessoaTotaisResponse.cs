using Despesas.API.Domain.Dtos;

namespace Despesas.API.RequestResponse
{
    public class PessoaTotaisResponse
    {
		public List<PessoaTotaisDto> Itens { get; set; } = new();
		public TotaisDto TotaisGerais { get; set; } = new TotaisDto();

	}
}
