using Despesas.API.Domain.Dtos;

namespace Despesas.API.RequestResponse
{
    public class CategoriaTotaisResponse
    {
        public List<CategoriaTotaisDto> Itens { get; set; } = new();
        public TotaisCategoriaDto TotaisGerais { get; set; } = new TotaisCategoriaDto();

	}
}
