using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Api_Insi_Web.Models;

public partial class Tutores
{
    public int IdTutor { get; set; }

   public string? Nombre { get; set; } = null!;

    public string? Apellido { get; set; } = null!;

    public string? Direccion { get; set; } = null!;

    public string ?Telefono { get; set; } = null!;

    public string? RelacionConEstudiante { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();

    [JsonIgnore]
    public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
}
