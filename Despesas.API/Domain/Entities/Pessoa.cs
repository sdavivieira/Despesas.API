using System.Text.Json.Serialization;

namespace Despesas.API.Domain.Entities
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }

		[JsonIgnore]
		public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();

	}
}
