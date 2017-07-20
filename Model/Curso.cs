namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Curso")]
    public partial class Curso
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Curso()
        {
            //Alumno = new HashSet<Alumno>();
            Alumno = new List<Alumno>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Alumno> Alumno { get; set; }
        public ICollection<Alumno> Alumno { get; set; }

        public List<Curso> Todo()
        {
            var cursos = new List<Curso>();
            try
            {
                using(var context = new TextContext())
                {
                    cursos = context.Curso.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return cursos;
        }
    }
}
