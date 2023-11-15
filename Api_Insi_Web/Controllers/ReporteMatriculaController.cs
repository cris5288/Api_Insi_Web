using Api_Insi_Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Insi_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteMatriculaController : ControllerBase
    {
        public readonly BdInsiContext _dbcontext;

        public ReporteMatriculaController(BdInsiContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("Lista")]
       public IActionResult Lista()
        {
            List<Matricula> lista = new List<Matricula>();

            try
            {
                lista = _dbcontext.Matriculas.Include(t => t.oTutor).ToList();
                lista = _dbcontext.Matriculas.Include(e => e.oEstudiante).ToList();

                return Ok(new { mensaje = "Lista de Matricula de estudiantes", response = lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, response = lista });
            }

        }

        [HttpGet]
        [Route("Obtener/{idMatricula:int}")]
        public IActionResult Obtener(int idMatricula)
        {
            Matricula oMatricula = _dbcontext.Matriculas.Find(idMatricula);

            if (oMatricula == null)
            {
                return BadRequest(" Matricula de Estudiante no encontrada");
            }

            try
            {
                oMatricula = _dbcontext.Matriculas.Include(t => t.oTutor).Where(m => m.IdMatricula == idMatricula).FirstOrDefault();
                oMatricula = _dbcontext.Matriculas.Include(e => e.oEstudiante).Where(m => m.IdMatricula == idMatricula).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = " Matricula de Estudiante encontrada", response = oMatricula });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }
        [HttpGet("porEstado/{estado}")]
        public ActionResult<List<Matricula>> ObtenerPorEstado(string estado)
        {
            var matriculas = _dbcontext.Matriculas .Include(e => e.oEstudiante)
                                                  .Where(m => m.EstadoMatricula == estado)
                                                  .ToList();

            if (matriculas.Count == 0)
            {
                return NotFound(new { mensaje = "No se encontraron matrículas con el estado especificado" });
            }

            return Ok(matriculas);
        }

        [HttpGet("porGrado/{gradoSolicitado}")]
        public ActionResult<List<Matricula>> ObtenerPorGradoSolicitado(string gradoSolicitado)
        {
            var matriculas = _dbcontext.Matriculas.Include(e => e.oEstudiante)
                                                  .Where(m => m.GradoSolicitado == gradoSolicitado)
                                                  .ToList();

            if (matriculas.Count == 0)
            {
                return NotFound(new { mensaje = "No se encontraron matrículas para el grado solicitado especificado" });
            }

            return Ok(matriculas);
        }


        [HttpDelete]
        [Route("Eliminar/{idMatricula:int}")]
        public IActionResult Eliminar(int idMatricula)
        {
            Matricula oMatricula = _dbcontext.Matriculas.Find(idMatricula);
            if (oMatricula == null)
            {
                return BadRequest("Estudiante no encontrado");
            }

            try
            {
                oMatricula = _dbcontext.Matriculas
                    .Include(t => t.oTutor)
                    .Include(e => e.oEstudiante)
                    .SingleOrDefault(m => m.IdMatricula == idMatricula);

                _dbcontext.RemoveRange(oMatricula.oTutor);
                _dbcontext.Remove(oMatricula.oEstudiante);
                _dbcontext.Remove(oMatricula);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Matricula del Estudiante eliminada correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

    }
}
