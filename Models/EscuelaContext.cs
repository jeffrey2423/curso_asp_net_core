using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.Entidades;
using Microsoft.EntityFrameworkCore;

namespace HolaMundoMVC.Models
{
    public class EscuelaContext : DbContext
    {
        public DbSet<Escuela> Escuelas { get; set; }
        public DbSet<Asignatura> Asignaturas { get; set; }
        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Evaluación> Evaluaciones { get; set; }

        public EscuelaContext(DbContextOptions<EscuelaContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var escuela = new Escuela();
            escuela.AñoDeCreacion = 2020;
            escuela.Nombre = "Escuela de Prueba";
            escuela.Id = Guid.NewGuid().ToString();
            escuela.Ciudad = "Cali";
            escuela.Pais = "Colombia";
            escuela.TipoEscuela = TiposEscuela.Secundaria;
            escuela.Dirección = "Cali-Colombia";

            //cargar cursos de la escuela

            var cursos = CargarCursos(escuela);

            //x cada curso cargar asignaturas
            var asignaturas = CargarAsignaturas(cursos);

            // x cada curso cargar alumnos
            var alumnos = CargarAlumnos(cursos);

            //cargar evaluaciones
            // var evaluaciones = CargarEvaluaciones(cursos,asignaturas,alumnos);

            modelBuilder.Entity<Escuela>().HasData(escuela);
            modelBuilder.Entity<Curso>().HasData(cursos.ToArray());
            modelBuilder.Entity<Asignatura>().HasData(asignaturas.ToArray());
            modelBuilder.Entity<Alumno>().HasData(alumnos.ToArray());
            // modelBuilder.Entity<Evaluación>().HasData(evaluaciones.ToArray());

            // modelBuilder.Entity<Asignatura>().HasData(
            //     new Asignatura
            //     {
            //         Nombre = "Matemáticas",
            //         Id = Guid.NewGuid().ToString()
            //     },
            //     new Asignatura
            //     {
            //         Nombre = "Educación Física",
            //         Id = Guid.NewGuid().ToString()
            //     },
            //     new Asignatura
            //     {
            //         Nombre = "Castellano",
            //         Id = Guid.NewGuid().ToString()
            //     },
            //     new Asignatura
            //     {
            //         Nombre = "Ciencias Naturales",
            //         Id = Guid.NewGuid().ToString()
            //     },
            //     new Asignatura
            //     {
            //         Nombre = "Programacion",
            //         Id = Guid.NewGuid().ToString()
            //     }

            // );

            // modelBuilder.Entity<Asignatura>().HasData(GenerarAlumnosAlAzar().ToArray());
        }

        private List<Alumno> CargarAlumnos(List<Curso> cursos)
        {
            //por cada curso cargar alumno
            var listAlum = new List<Alumno>();
            Random rdn = new Random();
            foreach (var curso in cursos)
            {
                int cant = rdn.Next(5, 20);
                var tmpAlumList = GenerarAlumnosAlAzar(cant, curso);
                listAlum.AddRange(tmpAlumList);
            }
            return listAlum;
        }

        private static List<Asignatura> CargarAsignaturas(List<Curso> cursos)
        {
            var listaCompleta = new List<Asignatura>();

            foreach (var curso in cursos)
            {
                var tmpList = new List<Asignatura>{
                new Asignatura() {Id = Guid.NewGuid().ToString(), Nombre = "Desarrollo de Aplicaciones con ASP.NET",
                                   CursoId = curso.Id },
                new Asignatura() {Id = Guid.NewGuid().ToString(), Nombre = "Carrera de JavaScript", CursoId = curso.Id },
                new Asignatura() {Id = Guid.NewGuid().ToString(), Nombre = "Seguridad Informática", CursoId = curso.Id },
                new Asignatura() {Id = Guid.NewGuid().ToString(), Nombre = "Bases de Datos", CursoId = curso.Id },
                new Asignatura() {Id = Guid.NewGuid().ToString(), Nombre = "Inteligencia Artificial y Machine Learning", CursoId = curso.Id },
                new Asignatura() {Id = Guid.NewGuid().ToString(), Nombre = "Escuela de Data Science", CursoId = curso.Id },
                new Asignatura() {Id = Guid.NewGuid().ToString(), Nombre = "Ingles", CursoId = curso.Id },
                };

                listaCompleta.AddRange(tmpList);
                // curso.Asignaturas = tmpList;
            }
            return listaCompleta;
        }

        private static List<Curso> CargarCursos(Escuela escuela)
        {
            return new List<Curso>(){
                    new Curso{
                        Id = Guid.NewGuid().ToString(),
                        Nombre = "101",
                        Jornada = TiposJornada.Mañana,
                        EscuelaId = escuela.Id,
                        Dirección = "Calle falsa"},
                    new Curso{Id = Guid.NewGuid().ToString(),
                        Nombre = "201", Jornada = TiposJornada.Mañana, EscuelaId = escuela.Id,
                        Dirección = "Calle falsa"},
                    new Curso{Id = Guid.NewGuid().ToString(), Nombre = "301", Jornada = TiposJornada.Mañana, EscuelaId = escuela.Id,
                        Dirección = "Calle falsa"},
                    new Curso{Id = Guid.NewGuid().ToString(),Nombre = "401", Jornada = TiposJornada.Mañana, EscuelaId = escuela.Id,
                        Dirección = "Calle falsa"},
                    new Curso{Id = Guid.NewGuid().ToString(),Nombre = "501", Jornada = TiposJornada.Mañana, EscuelaId = escuela.Id,
                        Dirección = "Calle falsa"}
                };
        }

        private List<Alumno> GenerarAlumnosAlAzar(int cantidad, Curso curso)
        {
            string[] nombre1 = { "Alba", "Felipa", "Eusebio", "Farid", "Donald", "Alvaro", "Nicolás" };
            string[] apellido1 = { "Ruiz", "Sarmiento", "Uribe", "Maduro", "Trump", "Toledo", "Herrera" };
            string[] nombre2 = { "Freddy", "Anabel", "Rick", "Murty", "Silvana", "Diomedes", "Nicomedes", "Teodoro" };

            var listaAlumnos = from n1 in nombre1
                               from n2 in nombre2
                               from a1 in apellido1
                               select new Alumno
                               {
                                   CursoId = curso.Id,
                                   Nombre = $"{n1} {n2} {a1}",
                                   Id = Guid.NewGuid().ToString()
                               };

            return listaAlumnos.OrderBy((al) => al.Id).Take(cantidad).ToList();
        }

        private List<Evaluación> CargarEvaluaciones(List<Curso> cursos, List<Asignatura> asignaturas, List<Alumno> alumnos, int numeroEvaluaciones = 5)
        {
            Random rnd = new Random();
            var listaEv = new List<Evaluación>();
            foreach (var curso in cursos)
            {
                foreach (var asignatura in asignaturas)
                {
                    foreach (var alumno in alumnos)
                    {
                        for (int i = 0; i < numeroEvaluaciones; i++)
                        {
                            int cantRandom = rnd.Next(0, 500);
                            var tmp = new List<Evaluación> {
                                new Evaluación
                                {
                                    Id = Guid.NewGuid().ToString(),
                                    Nombre = "Evaluación de " + asignatura.Nombre + " # " + (i + 1),
                                    AlumnoId = alumno.Id,
                                    Alumno = alumno,
                                    AsignaturaId = asignatura.Id,
                                    Asignatura = asignatura,
                                    Nota = (float)cantRandom / 100
                                }
                            };
                            listaEv.AddRange(tmp);
                        }
                    }
                }
            }

            return listaEv;
        }



    }
}