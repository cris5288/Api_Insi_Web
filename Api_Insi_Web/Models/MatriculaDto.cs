
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Api_Insi_Web.Models;

public partial class MatriculaDto
{
    public int IdMatricula { get; set; }
   
    public int? IdEstudiante { get; set; }
   
    public int ?IdTutor { get; set; }

    public DateTime ?FechaMatricula { get; set; } = null!;

    public string EstadoMatricula { get; set; } = null!;

    public string GradoSolicitado { get; set; } = null!;
    [JsonIgnore]
    public virtual EstudianteDto? oEstudiante { get; set; } = null!;
    [JsonIgnore]
    public virtual TutoresDto? oTutor { get; set; } = null!;
}
