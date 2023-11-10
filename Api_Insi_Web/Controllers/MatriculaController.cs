using Api_Insi_Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Api_Insi_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatriculaController : ControllerBase
    {

            public readonly BdInsiContext _dbcontext;

            public MatriculaController(BdInsiContext _context)
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

                    lista = _dbcontext.Matriculas./*Include(t => t.oTutor).*/ToList();
                    lista = _dbcontext.Matriculas./*Include(e => e.oEstudiante).*/ToList();

                    return StatusCode(StatusCodes.Status200OK, new { mensaje = "Lista de Matricula de estudiantes", response = lista });

                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
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
            var matriculas = _dbcontext.Matriculas
                                                  .Include(e => e.oEstudiante)
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



        [HttpPost]
        [Route("Guardar")]

        public IActionResult Guardar([FromBody] Matricula objeto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Lógica para asignar automáticamente un tutor
                    Tutores tutor = _dbcontext.Tutores.OrderByDescending(t => t.IdTutor).FirstOrDefault();
                    objeto.oTutor = tutor;
                    Estudiante estudiante = _dbcontext.Estudiantes.OrderByDescending(e => e.IdEstudiante).FirstOrDefault();
                    objeto.oEstudiante = estudiante;

                    _dbcontext.Matriculas.Add(objeto);
                    _dbcontext.SaveChanges();


                    int idTutorRecienAgregado = objeto.oTutor.IdTutor;
                    int idEstudianteRecienAgregado = objeto.oEstudiante.IdEstudiante;

                    return Ok(new { mensaje = "Matricula del Estudiante Guardada Correctamente" });
                }
                else
                {
                    return BadRequest(new { mensaje = "Los datos no son válidos. Por favor, revise que los datos sean correctos." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }

        }



        [HttpPut]
            [Route("Editar")]
            public IActionResult Editar([FromBody] Matricula objeto)
            {
                Matricula oMatricula = _dbcontext.Matriculas.Find(objeto.IdMatricula);

                if (oMatricula == null)
                {
                    return BadRequest("Matricula de Estudiante no encontrada");
                }
                try
                {

                    oMatricula.IdMatricula = objeto.IdMatricula is null ? oMatricula.IdMatricula : objeto.IdMatricula;
                    oMatricula.IdEstudiante = objeto.IdEstudiante is null ? oMatricula.IdEstudiante : objeto.IdEstudiante;
                    oMatricula.IdTutor = objeto.IdTutor is null ? oMatricula.IdTutor : objeto.IdTutor;
                    oMatricula.FechaMatricula = objeto.FechaMatricula is null ? oMatricula.FechaMatricula : objeto.FechaMatricula;
                    oMatricula.FechaMatricula = objeto.FechaMatricula is null ? oMatricula.FechaMatricula : objeto.FechaMatricula;
                    oMatricula.FechaMatricula = objeto.FechaMatricula is null ? oMatricula.FechaMatricula : objeto.FechaMatricula;
                    oMatricula.EstadoMatricula = objeto.EstadoMatricula is null ? oMatricula.EstadoMatricula : objeto.EstadoMatricula;
                    oMatricula.GradoSolicitado = objeto.GradoSolicitado is null ? oMatricula.GradoSolicitado : objeto.GradoSolicitado;

                    _dbcontext.Matriculas.Update(oMatricula);
                    _dbcontext.SaveChanges();

                    return StatusCode(StatusCodes.Status200OK, new { mensaje = "Matricula de Estudiante editada correctamente" });
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
                }
            }

        [HttpDelete]
        [Route("Eliminar/{idMatricula:int}")]
        public IActionResult Eliminar(int idMatricula)
        {
            try
            {
                Matricula oMatricula = _dbcontext.Matriculas
                    .Include(t => t.oTutor)
                    .Include(e => e.oEstudiante)
                    .FirstOrDefault(m => m.IdMatricula == idMatricula);

                if (oMatricula == null)
                {
                    return NotFound("Matrícula no encontrada");
                }

                _dbcontext.RemoveRange(oMatricula.oTutor, oMatricula.oEstudiante);
                _dbcontext.Remove(oMatricula);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Matrícula del estudiante eliminada correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }





    }
}


