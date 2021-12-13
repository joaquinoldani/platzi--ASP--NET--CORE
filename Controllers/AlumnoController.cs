using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using platzi_asp_net_core.Models;
using System.Collections.Generic;

namespace platzi_asp_net_core.Controllers
{
    public class AlumnoController : Controller
    {
        [Route("Alumno/Index/{alumnoId?}")]
        public IActionResult Index(string alumnoId)
        {
            ViewBag.Fecha = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(alumnoId))
            {
                var alumnos = from alum in _context.Alumnos
                             where alum.Id == alumnoId
                             select alum;

                return View(alumnos.SingleOrDefault());
            }
            else
            {
                return View("MultiAlumno", _context.Alumnos);
            } 
        }

        public  IActionResult MultiAlumno()
        {
            ViewBag.CosaDinamica = "La Monja";
            ViewBag.Fecha = DateTime.Now;
            return View("MultiAlumno", _context.Alumnos);
        }

        private EscuelaContext _context;

        public AlumnoController(EscuelaContext context)
        {
            _context = context;
        }
    }
}