using System;
using Microsoft.AspNetCore.Mvc;
using platzi_asp_net_core.Models;
using System.Collections.Generic;
using System.Linq;

namespace platzi_asp_net_core.Controllers
{
    public class CursoController : Controller
    {
        // tambien se puede crear la ruta de la siguiente manera
        // [Route("Asignatura/Index")] y [Route("Asignatura/Index/{asignaturaId}")]
        // o se puede resumir en una sola linea agregando un "?" al final para decir que
        // ese parametro puede estar o no
        [Route("Curso/Index/{cursoId?}")]
        public IActionResult Index(string cursoId)
        {
            ViewBag.Fecha = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(cursoId))
            {
                var curso = from cur in _context.Cursos
                             where cur.Id == cursoId
                             select cur;

                return View(curso.SingleOrDefault());
            }
            else
            {
                return View("MultiCurso", _context.Cursos);
            } 
        }

        public IActionResult MultiCurso()
        {
            ViewBag.Fecha = DateTime.Now;
            return View("MultiCurso", _context.Cursos);
        }

        public IActionResult Create()
        {
            ViewBag.Fecha = DateTime.Now;

            return View();
        }

        [HttpPost]
        public IActionResult Create(Curso curso)
        {
            ViewBag.Fecha = DateTime.Now;
            if(ModelState.IsValid)
            {
                var escuela = _context.Escuelas.FirstOrDefault();
                curso.EscuelaId = escuela.Id;

                _context.Cursos.Add(curso);
                _context.SaveChanges();
                ViewBag.MensajeExtra = "Curso creado con éxito";
                return View("Index", curso);
            }
            else
            {
                return View(curso);
            }
            
        }


        public IActionResult Update(string Id)
        {
            var cursoResponse = from curso in _context.Cursos
                                where curso.Id == Id
                                select curso;

            // Si no existe devuelvo la lista de todos los cursos
            if (string.IsNullOrWhiteSpace(Id)) return MultiCurso();
            
            return View(cursoResponse.SingleOrDefault());
        }

        [HttpPost]
        public IActionResult Update(string Id, Curso cursoForm)
        {   
            var curso = _context.Cursos.Find(Id);
            
            // Si el formulario es invalido lo vuelvo a mostrar con los datos que ya tiene
            if (!ModelState.IsValid)
            { 
                return View("Update", curso);
            } 

            // Busco el curso por Id
            var modelCurso = _context.Cursos.SingleOrDefault(c => c.Id == Id);

            // Si no existe devuelvo la lista de todos los cursos
            if (modelCurso == null) return MultiCurso();

            // Paso los campos del fomulario al modelo Curso y guardo los cambios
            modelCurso.Nombre = cursoForm.Nombre;
            modelCurso.Dirección = cursoForm.Dirección;
            modelCurso.Jornada = cursoForm.Jornada;
            
            _context.SaveChanges();
            
            // Informo que se haya actualizado correctamente
            ViewBag.MensajeExtra = "Curso Actualizado con éxito!";

            return View("Index",curso);
        }


        private EscuelaContext _context;

        public CursoController(EscuelaContext context)
        {
            _context = context;
        }
    }
}