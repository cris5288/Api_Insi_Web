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
            List<Tutores> lista = new List<Tutores>();

            try
            {
                int totalTutores = _dbcontext.Tutores.Count();
                lista = _dbcontext.Tutores.ToList();

                return Ok(new { mensaje = "Total de tutores", total = totalTutores, lista = lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, response = lista });
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
        public ActionResult Guardar([FromBody]Tutores objeto)
        {
            try
            {
                if (objeto == null)
                {
                    return BadRequest("No se proporcionó ningún dato del tutor.");
                }

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
