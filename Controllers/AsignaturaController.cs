using Microsoft.AspNetCore.Mvc;
using HolaMundoMVC.Models;
using System;
using System.Linq;
using CoreEscuela.Entidades;
using System.Collections.Generic;

namespace HolaMundoMVC.Controllers
{
    public class AsignaturaController : Controller
    {
        // public IActionResult Index()
        // {

        //     return View(
        //         // new Asignatura
        //         // {
        //         //     Nombre = "Programacion",
        //         //     Id = Guid.NewGuid().ToString()
        //         // }
        //         _context.Asignaturas.FirstOrDefault()
        //     );
        // }
        [Route("Asignatura/Index")]
        [Route("Asignatura/Index/{AsignaturaId}")]
        public IActionResult Index(string AsignaturaId)
        {
            if (!string.IsNullOrWhiteSpace(AsignaturaId))
            {
                var asignatura = from asig in _context.Asignaturas
                                 where asig.Id == AsignaturaId
                                 select asig;
                return View(

                asignatura.SingleOrDefault()
                );
            }
            else
            {
                return View("MultiAsignatura", _context.Asignaturas);
            }


        }


        public IActionResult MultiAsignatura()
        {
            var listaAsignaturas = new List<Asignatura>() {
                new Asignatura {
                Nombre = "Matemáticas",
                Id = Guid.NewGuid ().ToString ()
                },
                new Asignatura {
                Nombre = "Educación Física",
                Id = Guid.NewGuid ().ToString ()
                },
                new Asignatura {
                Nombre = "Castellano",
                Id = Guid.NewGuid ().ToString ()
                },
                new Asignatura {
                Nombre = "Ciencias Naturales",
                Id = Guid.NewGuid ().ToString ()
                },
                new Asignatura {
                Nombre = "Programacion",
                Id = Guid.NewGuid ().ToString ()
                }
            };


            ViewBag.CualquierCosa = "esto es cualquier cosa";
            ViewBag.Fecha = DateTime.Now;


            // return View("MultiAsignatura", listaAsignaturas);
            return View("MultiAsignatura", _context.Asignaturas);
        }

        private EscuelaContext _context;
        public AsignaturaController(EscuelaContext context)
        {
            _context = context;
        }
    }
}