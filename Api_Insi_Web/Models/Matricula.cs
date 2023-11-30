using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Linq;


namespace Api_Insi_Web.Models;

public partial class Matricula
{
    public int IdMatricula { get; set; }
    [JsonIgnore]
    public int? IdEstudiante { get; set; }
    [JsonIgnore]
    public int? IdTutor { get; set; }
    [Required(ErrorMessage = "El campo FechaMatricula es obligatorio.")]

    public DateTime? FechaMatricula { get; set; } = null!;
    [Required(ErrorMessage = "El campo EstadoMatricula es obligatorio.")]

    public string EstadoMatricula { get; set; } = null!;
    [Required(ErrorMessage = "El campo GradoSolicitado es obligatorio.")]

    public string GradoSolicitado { get; set; } = null!;
    public virtual Estudiante ?oEstudiante { get; set; } = null!;
    [JsonIgnore]
    public virtual Tutores ?oTutor { get; set; } = null!;
}
