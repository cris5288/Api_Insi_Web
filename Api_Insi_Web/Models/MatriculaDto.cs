
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Api_Insi_Web.Models;

public partial class MatriculaDto
{
    public int IdMatricula { get; set; }
   
    public int? IdEstudiante { get; set; }
   
    public int ?IdTutor { get; set; }
    [Required(ErrorMessage = "El campo FechaMatricula es obligatorio.")]

    public DateTime ?FechaMatricula { get; set; } = null!;
    [Required(ErrorMessage = "El campo EstadoMatricula es obligatorio.")]

    public string EstadoMatricula { get; set; } = null!;
    [Required(ErrorMessage = "El campo GradoSolicitado es obligatorio.")]

    public string GradoSolicitado { get; set; } = null!;
    [JsonIgnore]
    public virtual EstudianteDto? oEstudiante { get; set; } = null!;
    [JsonIgnore]
    public virtual TutoresDto? oTutor { get; set; } = null!;
}
