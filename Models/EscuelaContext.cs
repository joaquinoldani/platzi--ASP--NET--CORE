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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cargo los datos inicales de la escuela
            var escuela = new Escuela();
            escuela.AñoDeCreación = 2005;
            escuela.Id = Guid.NewGuid().ToString();
            escuela.Nombre = "Platzi School";
            escuela.Ciudad = "Bogota";
            escuela.Pais = "Colombia";
            escuela.Dirección = "Av. Siempre Viva 234";
            escuela.TipoEscuela = TiposEscuela.Secundaria;
            // Cargar Cursos de la escuela
            var cursos = CargarCursos(escuela);
            // y por cada curso, cargar asignaturas
            var asignaturas = CargarAsignaturas(cursos);
            // ademas por cada curso también tengo que cargar los alumnos
            var alumnos = CargarAlumnos(cursos);

            // Si no hay datos en la BD los cargo asi
            modelBuilder.Entity<Escuela>().HasData(escuela);
            modelBuilder.Entity<Curso>().HasData(cursos.ToArray());
            modelBuilder.Entity<Asignatura>().HasData(asignaturas.ToArray());
            modelBuilder.Entity<Alumno>().HasData(alumnos.ToArray());
        }

        private static List<Asignatura> CargarAsignaturas(List<Curso> cursos)
        {
            var listaCompleta = new List<Asignatura>();
            foreach (var curso in cursos)
            {
                var tempList = new List<Asignatura>(){
                    new Asignatura { Nombre = "Matemáticas", Id = Guid.NewGuid().ToString(), CursoId = curso.Id },
                    new Asignatura { Nombre = "Educación Física", Id = Guid.NewGuid().ToString(), CursoId = curso.Id },
                    new Asignatura { Nombre = "Castellano", Id = Guid.NewGuid().ToString(), CursoId = curso.Id },
                    new Asignatura { Nombre = "Ciencias Naturales", Id = Guid.NewGuid().ToString(), CursoId = curso.Id },
                    new Asignatura { Nombre = "Programacion", Id = Guid.NewGuid().ToString(), CursoId = curso.Id }
                };

                listaCompleta.AddRange(tempList);
                //curso.Asignaturas=tempList;
            }

            return listaCompleta;
        }

        private static List<Curso> CargarCursos(Escuela escuela)
        {
            return new List<Curso>(){
                new Curso(){
                    Id = Guid.NewGuid().ToString(),
                    EscuelaId = escuela.Id,
                    Nombre = "101",
                    Jornada = TiposJornada.Mañana
                },
                new Curso(){
                    Id = Guid.NewGuid().ToString(),
                    EscuelaId = escuela.Id,
                    Nombre = "201",
                    Jornada = TiposJornada.Mañana
                },
                new Curso(){
                    Id = Guid.NewGuid().ToString(),
                    EscuelaId = escuela.Id,
                    Nombre = "301",
                    Jornada = TiposJornada.Mañana
                },
                new Curso(){
                    Id = Guid.NewGuid().ToString(),
                    EscuelaId = escuela.Id,
                    Nombre = "401",
                    Jornada = TiposJornada.Tarde
                },
                new Curso(){
                    Id = Guid.NewGuid().ToString(),
                    EscuelaId = escuela.Id,
                    Nombre = "501",
                    Jornada = TiposJornada.Tarde
                },
            };
        }
    
        private List<Alumno> CargarAlumnos(List<Curso> cursos)
        {
            var listaAlumnos = new List<Alumno>();

            Random rnd = new Random();
            foreach (var curso in cursos)
            {
                // obtengo una cantidad random de alumnos por curso y se los agrego
                int cantRandom = rnd.Next(5, 20);
                var tmplist = GenerarAlumnosAlAzar(curso, cantRandom);
                listaAlumnos.AddRange(tmplist);
            }

            return listaAlumnos;
        }

        private List<Alumno> GenerarAlumnosAlAzar(Curso curso, int cantidad)
        {
            string[] nombre1 = { "Alba", "Felipa", "Eusebio", "Farid", "Donald", "Alvaro", "Nicolás" };
            string[] apellido1 = { "Ruiz", "Sarmiento", "Uribe", "Maduro", "Trump", "Toledo", "Herrera" };
            string[] nombre2 = { "Freddy", "Anabel", "Rick", "Murty", "Silvana", "Diomedes", "Nicomedes", "Teodoro" };

            var listaAlumnos = from n1 in nombre1
                               from n2 in nombre2
                               from a1 in apellido1
                               select new Alumno { 
                                   Nombre = $"{n1} {n2} {a1}", 
                                   Id = Guid.NewGuid().ToString(),
                                   CursoId = curso.Id
                                };

            return listaAlumnos.OrderBy((al) => al.Id).Take(cantidad).ToList();
        }
    }
}