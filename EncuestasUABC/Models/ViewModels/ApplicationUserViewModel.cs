﻿

using EncuestasUABC.Models.ViewModels.Catalogos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EncuestasUABC.Models
{
    public class ApplicationUserViewModel
    {
        [Required(ErrorMessage = "El {0} es requerido")]
        [MaxLength(80)]
        public string Nombre { get; set; }

        [Display(Name = "Apellido Paterno")]
        [Required(ErrorMessage = "El {0} es requerido")]
        [MaxLength(80)]
        public string ApellidoPaterno { get; set; }

        [Display(Name = "Apellido Materno")]
        [MaxLength(80)]
        public string ApellidoMaterno { get; set; }
        [Display(Name = "Rol")]
        [Required(ErrorMessage = "El {0} es requerido")]
        public int RolId { get; set; }
        public string UserName { get; set; }

        [Display(Name = "Correo")]
        [Required(ErrorMessage = "El {0} es requerido")]
        public string Email { get; set; }
        public bool Activo { get; set; } = true;

        public RolViewModel RolIdNavigation { get; set; }
        public AdministrativoViewModel Administrativo { get; set; }
        public AlumnoViewModel Alumno { get; set; }
        public EgresadoViewModel Egresado { get; set; }

    }

    public class AlumnoViewModel
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; }
        public string Matricula { get; set; }
        public int Semestre { get; set; }
        public string PeriodoIngreso { get; set; }
        [EmailAddress(ErrorMessage = "El correo no es válido")]
        public string CorreoAlterno { get; set; }
        public string Celular { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int? CarreraId { get; set; }

        public CarreraViewModel CarreraIdNavigation { get; set; }
        public ApplicationUserViewModel UsuarioIdNavigation { get; set; }

    }

    public class AdministrativoViewModel
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; }
        public string NumeroEmpleado { get; set; }
        [EmailAddress(ErrorMessage = "El correo no es válido")]
        public string Correo { get; set; }
        [EmailAddress(ErrorMessage = "El correo no es válido")]
        public string CorreoAlterno { get; set; }
        public string Telefono { get; set; }
        public ApplicationUserViewModel UsuarioIdNavigation { get; set; }
    }

    public class EgresadoViewModel
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; }
        public string PeriodoIngreso { get; set; }
        public string PeriodoEgreso { get; set; }
        [EmailAddress(ErrorMessage = "El correo no es válido")]
        public string Correo { get; set; }
        [EmailAddress(ErrorMessage = "El correo no es válido")]
        public string CorreoAlterno { get; set; }
        public string Facebook { get; set; }
        public string Otro { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public ApplicationUserViewModel UsuarioIdNavigation { get; set; }

    }
}