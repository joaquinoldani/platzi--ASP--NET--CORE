using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace platzi_asp_net_core.Models
{
    public class EscuelaContext : DbContext
    {
        // Lista de escuelas
        public DbSet<Escuela> Escuelas { get; set; }
        public DbSet<Asignatura> Asignaturas { get; set; }
        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Evaluacion> Evaluaciones { get; set; }

        // Constructor vacio
        public EscuelaContext(DbContextOptions<EscuelaContext> options) : base(options){}

        private List<Alumno> GenerarAlumnosAlAzar()
        {
            string[] nombre1 = { "Alba", "Felipa", "Eusebio", "Farid", "Donald", "Alvaro", "Nicolás" };
            string[] apellido1 = { "Ruiz", "Sarmiento", "Uribe", "Maduro", "Trump", "Toledo", "Herrera" };
            string[] nombre2 = { "Freddy", "Anabel", "Rick", "Murty", "Silvana", "Diomedes", "Nicomedes", "Teodoro" };

            var listaAlumnos = from n1 in nombre1
                               from n2 in nombre2
                               from a1 in apellido1
                               select new Alumno { Nombre = $"{n1} {n2} {a1}", Id = Guid.NewGuid().ToString() };

            return listaAlumnos.OrderBy((al) => al.Id).ToList();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var escuela = new Escuela();
            escuela.AñoDeCreación=2005;
            escuela.Id = Guid.NewGuid().ToString();
            escuela.Nombre="Platzi School";
            escuela.Ciudad="Bogota";
            escuela.Pais="Colombia";
            escuela.Dirección="Av. Siempre Viva 234";
            escuela.TipoEscuela=TiposEscuela.Secundaria;

            modelBuilder.Entity<Escuela>().HasData(escuela);

            modelBuilder.Entity<Asignatura>().HasData(
                new Asignatura { Nombre = "Matemáticas", Id = Guid.NewGuid ().ToString ()},
                new Asignatura { Nombre = "Educación Física", Id = Guid.NewGuid ().ToString ()},
                new Asignatura { Nombre = "Castellano", Id = Guid.NewGuid ().ToString ()},
                new Asignatura { Nombre = "Ciencias Naturales", Id = Guid.NewGuid ().ToString ()},
                new Asignatura { Nombre = "Programacion", Id = Guid.NewGuid ().ToString ()}
            );

            modelBuilder.Entity<Alumno>().HasData(GenerarAlumnosAlAzar().ToArray());
        }
    }
}