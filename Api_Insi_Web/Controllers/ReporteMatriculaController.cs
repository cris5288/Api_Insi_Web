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

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Lista de Matricula de estudiantes", response = lista });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
            }

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
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

    }
}
