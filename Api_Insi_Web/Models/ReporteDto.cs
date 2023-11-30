
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Linq;


namespace Api_Insi_Web.Models
{
    public class ReporteDto
    {
      //datos de las amtriculas

        public DateTime? FechaMatricula { get; set; } = null!;

        public string EstadoMatricula { get; set; } = null!;

        public string GradoSolicitado { get; set; } = null!;
        //datos de los estudiantes
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public DateTime? FechaNacimiento { get; set; } = null!;
        public string LugarNacimiento { get; set; } = null!;
        public string ZonaRecidencial { get; set; } = null!;
        public string PartidaNacimiento { get; set; } = null!;
        public int? Edad { get; set; }
        public string Genero { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string UltimoGradoAprobado { get; set; } = null!;
        public string EstaRepitiendoGrado { get; set; } = null!;
        //Datos del tutor
        public string Nombree { get; set; }

        public string Apellidoo { get; set; }

        public string Direccionn { get; set; }

        public string Telefonoo { get; set; }

        public string RelacionConEstudiante { get; set; }

    }
}
