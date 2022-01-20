using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CoreEscuela.Entidades;

namespace HolaMundoMVC.Models
{
    public class Curso:ObjetoEscuelaBase
    {
        [Required(ErrorMessage="El nombre es requerido")]
        [StringLength(5,ErrorMessage="El nombre sobrepasa los 10 caracteres")]
        public override string Nombre { get; set; }
        public TiposJornada Jornada { get; set; }
        public List<Asignatura> Asignaturas{ get; set; }
        public List<Alumno> Alumnos{ get; set; }

        [Required(ErrorMessage="La direccion es requerida")]
        [MinLength(10, ErrorMessage="La longitud minima de la direccion es 10")]
        public string Dirección { get; set; }

        public string EscuelaId {get; set;}
        public Escuela Escuela {get; set;}

    }
}