using System;
using Microsoft.AspNetCore.Mvc;
using platzi_asp_net_core.Models;
using System.Collections.Generic;
using System.Linq;

namespace platzi_asp_net_core.Controllers
{
    public class AsignaturaController : Controller
    {
        public  IActionResult Index()
        {
            return View( _context.Asignaturas.FirstOrDefault());
        }
        public  IActionResult MultiAsignatura()
        {
            ViewBag.CosaDinamica = "La Monja";
            ViewBag.Fecha = DateTime.Now;
            return View("MultiAsignatura", _context.Asignaturas);
        }

        private EscuelaContext _context;

        public AsignaturaController(EscuelaContext context)
        {
            _context = context;
        }
    }
}