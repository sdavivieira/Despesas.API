using static Despesas.API.Domain.Enum.Enums;

namespace Despesas.API.RequestResponse
{
    public class CategoriaRequest
    {
		public string Descricao { get; set; }
		public FinalidadeCategoria FinalidadeCategoria { get; set; }
	}
}
