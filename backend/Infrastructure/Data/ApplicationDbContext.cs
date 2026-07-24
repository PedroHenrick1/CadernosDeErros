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
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Usuario 1 -> N Materias
            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Materias)
                .WithOne(m => m.Usuario)
                .HasForeignKey(m => m.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Usuario 1 -> N Assuntos
            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Assuntos)
                .WithOne(a => a.Usuario)
                .HasForeignKey(a => a.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            // Usuario 1 -> N Erros
            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Erros)
                .WithOne(e => e.Usuario)
                .HasForeignKey(e => e.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            // Matéria 1 -> N Assuntos
            modelBuilder.Entity<Materia>()
                .HasMany(m => m.Assuntos)
                .WithOne(a => a.Materia)
                .HasForeignKey(a => a.MateriaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Assunto 1 -> N Erros
            modelBuilder.Entity<Assunto>()
                .HasMany(a => a.Erros)
                .WithOne(e => e.Assunto)
                .HasForeignKey(e => e.AssuntoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
