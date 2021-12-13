using System;
using System.Collections.Generic;

namespace platzi_asp_net_core.Models
{
    public class Asignatura:ObjetoEscuelaBase
    {
        // referencio a la clase padre
        public string CursoId { get; set; }
        public Curso Curso { get; set; }

        public List<Evaluacion> Evaluaciones { get; set; }
        
    }
}