using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Api_Insi_Web.Models;

public partial class Matricula
{
    public int? IdMatricula { get; set; }

    public int? IdEstudiante { get; set; }

    public int? IdTutor { get; set; }

    public DateTime ?FechaMatricula { get; set; } = null!;

    public string? EstadoMatricula { get; set; } = null!;

    public string ?GradoSolicitado { get; set; } = null!;

    public virtual Estudiante ?oEstudiante { get; set; } = null!;
    [JsonIgnore]
    public virtual Tutores ?oTutor { get; set; } = null!;
}
