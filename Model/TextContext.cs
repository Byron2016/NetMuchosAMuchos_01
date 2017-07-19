namespace Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TextContext : DbContext
    {
        public TextContext()
            : base("name=TextContext")
        {
        }

        public virtual DbSet<Alumno> Alumno { get; set; }
        public virtual DbSet<Curso> Curso { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alumno>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Alumno>()
                .Property(e => e.Apellido)
                .IsUnicode(false);

            modelBuilder.Entity<Alumno>()
                .HasMany(e => e.Curso)
                .WithMany(e => e.Alumno)
                .Map(m => m.ToTable("AlumnoCurso"));

            modelBuilder.Entity<Curso>()
                .Property(e => e.Nombre)
                .IsUnicode(false);
        }
    }
}
