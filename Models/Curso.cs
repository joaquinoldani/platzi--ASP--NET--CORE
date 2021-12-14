using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace platzi_asp_net_core.Models
{
    public class Curso:ObjetoEscuelaBase
    {
        [Required(ErrorMessage ="El nombre del curso es requerido")]
        [StringLength(5)]
        public override string Nombre { get; set; }
        public TiposJornada Jornada { get; set; }
        public List<Asignatura> Asignaturas{ get; set; }
        public List<Alumno> Alumnos{ get; set; }
        
        [Display(Prompt = "Direccion de correspondencia", Name ="Address")]
        [Required(ErrorMessage ="Se requiere incluir una direccion")]
        [MinLength(10, ErrorMessage = "La longitud minima es 10")]
        public string Dirección { get; set; }

        // por convencion la relacion con la escuela se escribe con ese nombre
        public string EscuelaId { get; set; } 

        // pero ademas quiero traer el objeto Escuela de la siguiente manera
        public Escuela Escuela { get; set; }
    }
}