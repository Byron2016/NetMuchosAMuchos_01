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
        // GET: Home
        public ActionResult Index()
        {
            return View(alumno.Listar());
        }


        public ActionResult Crud(int id)
        {
            return View(alumno.Obtener(id));
        }

        public ActionResult Ver(int id)
        {
            return View(alumno.Obtener(id));
        }
    }
}