namespace CadernosDeErros.Entities
{
    public class Erro
    {
        public int Id { get; set; }
        public string Questao { get; set; } = string.Empty;
        public string? RespostaCorreta { get; set; }
        public string? MinhaResposta { get; set; }
        public string? Explicacao { get; set; }
        public string? Observacoes { get; set; }
        public int AssuntoId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataErro { get; set; } = DateTime.UtcNow;
        public DateTime? DataRevisao { get; set; }
        public bool Revisado { get; set; } = false;

        // Navigation properties
        public Assunto Assunto { get; set; } = null!;
        public Usuario Usuario { get; set; } = null!;
    }
}
