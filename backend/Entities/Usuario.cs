using System.ComponentModel.DataAnnotations;

namespace CadernosDeErros.Entities
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string SenhaHash { get; set; } = string.Empty;

        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ICollection<Materia> Materias { get; set; } = new List<Materia>();
        public ICollection<Assunto> Assuntos { get; set; } = new List<Assunto>();
        public ICollection<Erro> Erros { get; set; } = new List<Erro>();
    }
}
