
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Api_Insi_Web.Models;

public partial class EstudianteDto
{
    public int IdEstudiante { get; set; }
    
    public int IdTutor { get; set; }
    [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
    public string Nombre { get; set; } = null!;
    [Required(ErrorMessage = "El campo Apellido es obligatorio.")]
    public string Apellido { get; set; } = null!;
    [Required(ErrorMessage = "El campo FechaNacimiento es obligatorio.")]
    public DateTime? FechaNacimiento { get; set; } = null!;
    [Required(ErrorMessage = "El campo LugarNacimiento es obligatorio.")]
    public string LugarNacimiento { get; set; } = null!;
    [Required(ErrorMessage = "El campo ZonaRecidencial es obligatorio.")]
    public string ZonaRecidencial { get; set; } = null!;
    [Required(ErrorMessage = "El campo PartidaNacimiento es obligatorio.")]
    public string PartidaNacimiento { get; set; } = null!;
    public int? Edad { get; set; }
    [Required(ErrorMessage = "El campo Genero es obligatorio.")]
    public string Genero { get; set; } = null!;
    [Required(ErrorMessage = "El campo Direccion es obligatorio.")]
    public string Direccion { get; set; } = null!;
    [RegularExpression(@"^\d+$", ErrorMessage = "El campo Telefono solo debe contener números.")]
    public string Telefono { get; set; } = null!;
    [Required(ErrorMessage = "El campo UltimoGradoAprobado es obligatorio.")]
    public string UltimoGradoAprobado { get; set; } = null!;
    [Required(ErrorMessage = "El campo EstaRepitiendoGrado es obligatorio.")]
    public string EstaRepitiendoGrado { get; set; } = null!;
    [JsonIgnore]
    public virtual Tutores? oTutor { get; set; }
    [JsonIgnore]
    public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
}
