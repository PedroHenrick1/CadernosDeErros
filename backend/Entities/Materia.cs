namespace CadernosDeErros.Entities
{
    public class Materia
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int UsuarioId { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Usuario Usuario { get; set; } = null!;
        public ICollection<Assunto> Assuntos { get; set; } = new List<Assunto>();
    }
}
