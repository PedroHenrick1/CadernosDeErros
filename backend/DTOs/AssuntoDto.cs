namespace CadernosDeErros.DTOs
{
    public class AssuntoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int MateriaId { get; set; }
        public string NomeMateria { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
        public int QuantidadeErros { get; set; }
    }

    public class CreateAssuntoDto
    {
        public string Nome { get; set; } = string.Empty;
        public int MateriaId { get; set; }
    }

    public class UpdateAssuntoDto
    {
        public string? Nome { get; set; }
        public int? MateriaId { get; set; }
    }
}
