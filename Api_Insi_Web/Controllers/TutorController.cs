using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api_Insi_Web.Models;
using AutoMapper;

namespace Api_Insi_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutorController : ControllerBase
    {
        public readonly BdInsiContext _dbcontext;

        public TutorController(BdInsiContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("Lista")]
        public ActionResult Lista()
        {
            List<TutoresDto> listaTutoresDto = new List<TutoresDto>();

            try
            {
                int total = _dbcontext.Tutores.Count();
                var tutores = _dbcontext.Tutores.ToList();

                foreach (var tutor in tutores)
                {
                    var tutorDto = new TutoresDto
                    {
                        IdTutor = tutor.IdTutor,
                        Nombre = tutor.Nombre,
                        Apellido = tutor.Apellido,
                        Direccion = tutor.Direccion,
                        Telefono = tutor.Telefono,
                        RelacionConEstudiante = tutor.RelacionConEstudiante,
                        
                    };

                    listaTutoresDto.Add(tutorDto);
                }

                return Ok(new { mensaje = "Lista de tutores", TotalTutores = total, lista = listaTutoresDto });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, response = listaTutoresDto });
            }
        }



        [HttpGet]
        [Route("Obtener/{idTutores:int}")]
        public ActionResult Obtener(int idTutores)
        {
            try
            {
                Tutores oTutores = _dbcontext.Tutores.Find(idTutores);

                if (oTutores == null)
                {
                    return BadRequest("Tutor no encontrado");
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Tutor encontrado", response = oTutores });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }
       
        [HttpPost]
        [Route("Guardar")]
        public ActionResult Guardar([FromBody] TutoresDto objetoDto)
        {
            try
            {
                if (objetoDto == null)
                {
                    return BadRequest("No se proporcionó ningún dato del tutor.");
                }

                var objeto = new Tutores
                {
                    IdTutor = objetoDto.IdTutor,
                    Nombre = objetoDto.Nombre,
                    Apellido = objetoDto.Apellido,
                    Direccion = objetoDto.Direccion,
                    Telefono = objetoDto.Telefono,
                    RelacionConEstudiante = objetoDto.RelacionConEstudiante
                   
                };

                _dbcontext.Tutores.Add(objeto);
                _dbcontext.SaveChanges();

                return CreatedAtAction(nameof(Guardar), new { id = objeto.IdTutor }, new { mensaje = "Tutor guardado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Se produjo un error al guardar el tutor." });
            }
        }
        
        [HttpPut]
        [Route("Editar")]
        public ActionResult Editar([FromBody] Tutores objeto3)
        {

            Tutores oTutores = _dbcontext.Tutores.Find(objeto3.IdTutor);

            if (oTutores == null)
            {
                return BadRequest("Tutor no encontrado");
            }
            try
            {
                oTutores.Nombre = objeto3.Nombre is null ? oTutores.Nombre : objeto3.Nombre;
                oTutores.Apellido = objeto3.Apellido is null ? oTutores.Apellido : objeto3.Apellido;
                oTutores.Direccion = objeto3.Direccion is null ? oTutores.Direccion : objeto3.Direccion;
                oTutores.Telefono = objeto3.Telefono is null ? oTutores.Telefono : objeto3.Telefono;
                oTutores.RelacionConEstudiante = objeto3.RelacionConEstudiante is null ? oTutores.RelacionConEstudiante : objeto3.RelacionConEstudiante;

                _dbcontext.Tutores.Update(oTutores);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Datos de Tutor editados correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message});
            }

        }

        [HttpDelete]
        [Route("Eliminar/{idTutores:int}")]
        public ActionResult Eliminar(int idTutores)
        {
            Tutores oTutores = _dbcontext.Tutores.Find(idTutores);
            if (oTutores == null)
            {
                return BadRequest("Tutor no encontrado");
            }

            try
            {
                _dbcontext.Tutores.Remove(oTutores);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Tutor eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }
    }
}
