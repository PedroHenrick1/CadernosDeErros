using CadernosDeErros.Entities;
using Microsoft.EntityFrameworkCore;

namespace CadernosDeErros.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Materia> Materias { get; set; }
        public DbSet<Assunto> Assuntos { get; set; }
        public DbSet<Erro> Erros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // Matéria 1 -> N Assuntos
            modelBuilder.Entity<Materia>()
                .HasMany(m => m.Assuntos)
                .WithOne(a => a.Materia)
                .HasForeignKey(a => a.MateriaId)
                .OnDelete(DeleteBehavior.Cascade); // Se deletar matéria, deleta os assuntos

            // Assunto 1 -> N Erros
            modelBuilder.Entity<Assunto>()
                .HasMany(a => a.Erros)
                .WithOne(e => e.Assunto)
                .HasForeignKey(e => e.AssuntoId)
                .OnDelete(DeleteBehavior.Restrict); // Evita deletar assunto se tiver erros (segurança)
        }
    }
}
