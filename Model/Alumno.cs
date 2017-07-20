namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Data.SqlClient;
    using System.Linq; //este se añade para que el .toList no d error.

    [Table("Alumno")]
    public partial class Alumno
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Alumno()
        {
            //Curso = new HashSet<Curso>();
            Cursos = new List<Curso>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage ="Debe meter nombre")]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe meter apellido")]
        [StringLength(100)]
        public string Apellido { get; set; }

        [StringLength(100)]
        public string Descripcion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Curso> Curso { get; set; }

        public ICollection<Curso> Cursos { get; set; }

        public List<Alumno> Listar()
        {
            var alumnos = new List<Alumno>();
            try
            {
                using (var context = new TextContext())
                {
                    alumnos = context.Alumno.ToList();
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return alumnos;
        }

        public Alumno Obtener(int id)
        {
            var alumno = new Alumno();
            try
            {
                using (var context = new TextContext())
                {
                    alumno = context.Alumno
                                    .Include("Cursos")
                                    .Where(x => x.Id == id)
                                    .Single();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return alumno;
        }

        public void Guardar()
        {
            var alumno = new Alumno();
            try
            {
                
                using (var context = new TextContext())
                {
                    if (this.Id == 0)
                    {
                        context.Entry(this).State = EntityState.Added;
                    }
                    else
                    {
                        context.Database.ExecuteSqlCommand(
                            "DELETE FROM AlumnoCurso WHERE Alumno_Id = @Id",
                            new SqlParameter("Id", this.Id)
                        );

                        var cursoBK = this.Cursos;

                        this.Cursos = null;
                        context.Entry(this).State = EntityState.Modified;
                        this.Cursos = cursoBK;
                    }
                    foreach (var c in this.Cursos)
                    {
                        context.Entry(c).State = EntityState.Unchanged; //min 17.35 insertando actualizando con relaciones
                    }
                        

                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


    }
}
