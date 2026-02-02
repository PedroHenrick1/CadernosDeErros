namespace CadernosDeErros.Entities
{
    public class Assunto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; } = string.Empty;
        public int MateriaId { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Materia Materia { get; set; } = null!;
        public ICollection<Erro> Erros { get; set; } = new List<Erro>();
    }
}
