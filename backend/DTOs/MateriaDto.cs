namespace CadernosDeErros.DTOs
{
    public class MateriaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
        public int QuantidadeAssuntos { get; set; }
    }

    public class CreateMateriaDto
    {
        public string Nome { get; set; } = string.Empty;
    }

    public class UpdateMateriaDto
    {
        public string? Nome { get; set; }
    }
}
