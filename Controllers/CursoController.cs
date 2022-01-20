using Microsoft.AspNetCore.Mvc;
using HolaMundoMVC.Models;
using System;
using System.Linq;
using CoreEscuela.Entidades;
using System.Collections.Generic;

namespace HolaMundoMVC.Controllers
{
    public class CursoController : Controller
    {
        // [Route("Alumno/Index")]
        // [Route("Alumno/Index/{AsignaturaId}")]
        public IActionResult Index(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var curso = from cur in _context.Cursos
                                 where cur.Id == id
                                 select cur;
                return View(
                curso.SingleOrDefault()
                );
            }
            else
            {
                return View("MultiCurso", _context.Cursos);
            }


        }


        public IActionResult MultiCurso()
        {
            // var listaAlumnos = new List<Alumno>() {
            //     new Alumno {
            //     Nombre = "jose",
            //     UniqueId = Guid.NewGuid ().ToString ()
            //     },
            //     new Alumno {
            //     Nombre = "Manuel",
            //     UniqueId = Guid.NewGuid ().ToString ()
            //     },
            //     new Alumno {
            //     Nombre = "Daniela",
            //     UniqueId = Guid.NewGuid ().ToString ()
            //     },
            //     new Alumno {
            //     Nombre = "Jeffrey",
            //     UniqueId = Guid.NewGuid ().ToString ()
            //     },
            //     new Alumno {
            //     Nombre = "John",
            //     UniqueId = Guid.NewGuid ().ToString ()
            //     }
            // };
            var listaAlumnos = GenerarAlumnosAlAzar();

            ViewBag.CualquierCosa = "esto es cualquier cosa";
            ViewBag.Fecha = DateTime.Now;


            // return View("MultiAlumno", listaAlumnos);
            return View("MultiCurso", _context.Cursos);
        }

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

        public IActionResult Create()
        {

            ViewBag.Fecha = DateTime.Now;

            return View();
        }

        [HttpPost]
        public IActionResult Create(Curso curso)
        {
            ViewBag.Fecha = DateTime.Now;
            ViewBag.mensaje = null;
            ViewBag.err = false;

            if(ModelState.IsValid){
            var escuela = _context.Escuelas.FirstOrDefault();
            
            curso.EscuelaId = escuela.Id;
            _context.Cursos.Add(curso);
            _context.SaveChanges();
            ViewBag.mensaje = "Curso guardado con exito";
            return View("Index", curso);

            }else{
                ViewBag.err = true;
                ViewBag.mensaje = "El curso no es valido";
                return View(curso);
            }
            

        }

        private EscuelaContext _context;
        public CursoController(EscuelaContext context)
        {
            _context = context;
        }
    }
}