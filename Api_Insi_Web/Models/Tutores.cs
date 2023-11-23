using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Api_Insi_Web.Models;

public partial class Tutores
{
    public int IdTutor { get; set; }
    [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
    public string Nombre { get; set; } = null!;
    [Required(ErrorMessage = "El campo Apellido es obligatorio.")]
    public string Apellido { get; set; } = null!;
    [Required(ErrorMessage = "El campo Dirección es obligatorio.")]
    public string Direccion { get; set; } = null!; 
    [Required(ErrorMessage = "El campo Telefono es obligatorio.")]

    [RegularExpression(@"^\d+$", ErrorMessage = "El campo Telefono solo debe contener números.")]
    public string Telefono { get; set; } = null!;
    [Required(ErrorMessage = "El campo RelacionConEstudiante es obligatorio.")]

    public string RelacionConEstudiante { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();

    [JsonIgnore]
    public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
}
