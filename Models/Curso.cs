using System;
using System.Collections.Generic;


namespace platzi_asp_net_core.Models
{
    public class Curso:ObjetoEscuelaBase
    {
        public TiposJornada Jornada { get; set; }
        public List<Asignatura> Asignaturas{ get; set; }
        public List<Alumno> Alumnos{ get; set; }

        public string Dirección { get; set; }

        // por convencion la relacion con la escuela se escribe con ese nombre
        public string EscuelaId { get; set; } 

        // pero ademas quiero traer el objeto Escuela de la siguiente manera
        public Escuela Escuela { get; set; }
    }
}