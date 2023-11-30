using System;
using Api_Insi_Web.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;



namespace Api_Insi_Web.Models
{
    public partial class TutoresDto
    {
        public int IdTutor { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo Apellido es obligatorio.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El campo Dirección es obligatorio.")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "El campo Telefono es obligatorio.")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "El campo Telefono debe contener exactamente 8 números.")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El campo RelacionConEstudiante es obligatorio.")]
        public string RelacionConEstudiante { get; set; }

        [JsonIgnore]
        public virtual ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();

        [JsonIgnore]
        public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
    }
}
