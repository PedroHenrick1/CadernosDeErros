namespace CadernosDeErros.DTOs
{
    public class ErroDto
    {
        public int Id { get; set; }
        public string Questao { get; set; } = string.Empty;
        public string? RespostaCorreta { get; set; }
        public string? MinhaResposta { get; set; }
        public string? Explicacao { get; set; }
        public string? Observacoes { get; set; }
        public int AssuntoId { get; set; }
        public string NomeAssunto { get; set; } = string.Empty;
        public string NomeMateria { get; set; } = string.Empty;
        public DateTime DataErro { get; set; }
        public DateTime? DataRevisao { get; set; }
        public bool Revisado { get; set; }
    }

    public class CreateErroDto
    {
        public string Questao { get; set; } = string.Empty;
        public string? RespostaCorreta { get; set; }
        public string? MinhaResposta { get; set; }
        public string? Explicacao { get; set; }
        public string? Observacoes { get; set; }
        public int AssuntoId { get; set; }
    }

    public class UpdateErroDto
    {
        public string? Questao { get; set; }
        public string? RespostaCorreta { get; set; }
        public string? MinhaResposta { get; set; }
        public string? Explicacao { get; set; }
        public string? Observacoes { get; set; }
        public int? AssuntoId { get; set; }
    }
}
