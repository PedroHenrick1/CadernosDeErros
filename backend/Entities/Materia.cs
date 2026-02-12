namespace CadernosDeErros.Entities
{
    public class Materia
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        // Navigation property
        public ICollection<Assunto> Assuntos { get; set; } = new List<Assunto>();
    }
}
