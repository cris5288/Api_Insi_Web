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
        public ActionResult Lista()
        {
            List<MatriculaDto> listaMatriculaDto = new List<MatriculaDto>();

            try
            {
                int total = _dbcontext.Matriculas.Count();
                var matriculas = _dbcontext.Matriculas.ToList();

                foreach (var matricula in matriculas)
                {
                    var matriculaDto = new MatriculaDto
                    {
                        IdMatricula = matricula.IdMatricula,
                        IdEstudiante = matricula.IdEstudiante,
                        IdTutor = matricula.IdTutor,
                        FechaMatricula = matricula.FechaMatricula,
                        EstadoMatricula = matricula.EstadoMatricula,
                        GradoSolicitado = matricula.GradoSolicitado,
                    };

                    listaMatriculaDto.Add(matriculaDto);
                }

                return Ok(new { mensaje = "Lista de matrículas", TotalMatriculas = total, lista = listaMatriculaDto });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, response = listaMatriculaDto });
            }
        }


        [HttpGet]
        [Route("Obtener/{idMatricula:int}")]
        public ActionResult Obtener(int idMatricula)
        {
            try
            {
                Matricula oMatricula = _dbcontext.Matriculas.Find(idMatricula);

                if (oMatricula == null)
                {
                    return NotFound(" Matricula de Estudiante no encontrada");
                }
                MatriculaDto matriculaDto = new MatriculaDto
                {
                    IdMatricula = oMatricula.IdMatricula,
                    IdEstudiante = oMatricula.IdEstudiante,
                    IdTutor = oMatricula.IdTutor,
                    FechaMatricula = oMatricula.FechaMatricula,
                    EstadoMatricula = oMatricula.EstadoMatricula,
                    GradoSolicitado = oMatricula.GradoSolicitado,
                };

                return StatusCode(StatusCodes.Status200OK, new { mensaje = " Matricula de Estudiante encontrada", response = matriculaDto });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }


        [HttpPost]
        [Route("Guardar")]
        public ActionResult Guardar([FromBody] Matricula objeto)
        {
            try
            {
                
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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("Editar")]
        public ActionResult Editar([FromBody] Matricula objeto)
        {
            Matricula oMatricula = _dbcontext.Matriculas.Find(objeto.IdMatricula);

            if (oMatricula == null)
            {
                return BadRequest("Matricula de Estudiante no encontrada");
            }
            try
            {


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

        [HttpDelete("Eliminar/{idMatricula}")]
        public IActionResult EliminarMatricula(int idMatricula)
        {
            try
            {
                var matricula = _dbcontext.Matriculas
                    .Include(m => m.oEstudiante)
                    .Include(m => m.oTutor)
                    .FirstOrDefault(m => m.IdMatricula == idMatricula);

                if (matricula == null)
                {
                    return NotFound();
                }

                _dbcontext.Matriculas.Remove(matricula);
                _dbcontext.Estudiantes.Remove(matricula.oEstudiante);
                _dbcontext.Tutores.Remove(matricula.oTutor);
                _dbcontext.SaveChanges();

                return Ok("La matrícula y los registros relacionados se eliminaron correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }


        }



    }   
}


