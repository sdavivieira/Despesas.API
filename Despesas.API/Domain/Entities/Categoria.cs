using static Despesas.API.Domain.Enum.Enums;

namespace Despesas.API.Domain.Entities
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public FinalidadeCategoria FinalidadeCategoria { get; set; }
	}
}
