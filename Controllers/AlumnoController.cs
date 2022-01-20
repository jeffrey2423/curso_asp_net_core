using Microsoft.AspNetCore.Mvc;
using HolaMundoMVC.Models;
using System;
using System.Linq;
using CoreEscuela.Entidades;
using System.Collections.Generic;

namespace HolaMundoMVC.Controllers
{
    public class AlumnoController : Controller
    {
        // [Route("Alumno/Index")]
        // [Route("Alumno/Index/{AsignaturaId}")]
        public IActionResult Index(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var alumno = from alum in _context.Alumnos
                                 where alum.Id == id
                                 select alum;
                return View(
                alumno.SingleOrDefault()
                );
            }
            else
            {
                return View("MultiAlumno", _context.Alumnos);
            }


        }


        public IActionResult MultiAlumno()
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
            return View("MultiAlumno", _context.Alumnos);
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

        private EscuelaContext _context;
        public AlumnoController(EscuelaContext context)
        {
            _context = context;
        }
    }
}