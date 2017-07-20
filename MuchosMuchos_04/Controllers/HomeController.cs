using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MuchosMuchos_04.Controllers
{
    public class HomeController : Controller
    {
        private Alumno alumno = new Alumno();
        private Curso curso = new Curso();

        // GET: Home
        public ActionResult Index()
        {
            return View(alumno.Listar());
        }


        public ActionResult Ver(int id)
        {
            return View(alumno.Obtener(id));
        }

        public ActionResult Crud(int id = 0)
        {
            ViewBag.Cursos = curso.Todo();
            return View(
                id > 0 ? alumno.Obtener(id)
                       : alumno);

        }
        public ActionResult Guardar(Alumno model, int[] cursos = null)
        {
            if(cursos != null)
            {
                foreach (var c in cursos)
                {
                    model.Cursos.Add(new Curso { Id = c });
                }
                    
            }
            else
            {
                ModelState.AddModelError("Cursos", "Debe seleccionar al menos un curso");
                return View("~/views/home/crud.cshtml", model);
            }
            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/home/crud/" + model.Id);
            }
            else
            {
                ViewBag.Cursos = curso.Todo();
                return View("~/views/home/crud.cshtml",model);
            }
            
        }
    }
}