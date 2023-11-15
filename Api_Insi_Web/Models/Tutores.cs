using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Api_Insi_Web.Models;

public partial class Tutores
{
    public int IdTutor { get; set; }

    [Required(ErrorMessage = "El campo 'Nombre' es obligatorio.")]
    public string Nombre { get; set; } = null!;

    [Required(ErrorMessage = "El campo 'Apellido' es obligatorio.")]
    public string Apellido { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    [RegularExpression(@"^\d{8}$", ErrorMessage = "El campo 'Telefono' debe ser un número de 8 dígitos.")]
    public string Telefono { get; set; } = null!;

    [Required(ErrorMessage = "El campo 'RelacionConEstudiante' es obligatorio.")]
    public string RelacionConEstudiante { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();

    [JsonIgnore]
    public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
}
