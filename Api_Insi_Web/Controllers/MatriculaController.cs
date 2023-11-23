using Api_Insi_Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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

                return CreatedAtAction(nameof(Guardar), new { id = objeto.IdMatricula }, new { mensaje = "Matricula guardada correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }


        [HttpPut]
        [Route("Editar/{idMatricula:int}")]
        public ActionResult Editar(int idMatricula,[FromBody] Matricula objeto)
        {
            Matricula oMatricula = _dbcontext.Matriculas.Find(idMatricula);

            if (oMatricula == null)
            {
                return BadRequest("Matricula de Estudiante no encontrada");
            }
            try
            {

                oMatricula.FechaMatricula = objeto.FechaMatricula;
                oMatricula.EstadoMatricula = objeto.EstadoMatricula;
                oMatricula.GradoSolicitado = objeto.GradoSolicitado;

                _dbcontext.Matriculas.Update(oMatricula);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Matricula de Estudiante editada correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPatch("Editar/{idMatricula:int}")]
        public ActionResult ActualizarMatricula(int idMatricula, [FromBody] JsonPatchDocument<MatriculaDto> patchDocument)
        {
            // Obtén el tutor de tu almacén de datos o base de datos
            var oMatricula = ObtenerMatriculaPorId(idMatricula);

            if (oMatricula == null)
            {
                return NotFound();
            }

            // Crea un objeto TutoresDto para aplicar los cambios parciales
            var matriculaDto = new MatriculaDto()
            {
                FechaMatricula = oMatricula.FechaMatricula,
                EstadoMatricula = oMatricula.EstadoMatricula,
                GradoSolicitado = oMatricula.GradoSolicitado,
            };

            // Aplica los cambios parciales al tutorDto utilizando el patchDocument
            patchDocument.ApplyTo(matriculaDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(matriculaDto))
            {
                return BadRequest(ModelState);
            }

            // Aplica los cambios al tutor original
            oMatricula.FechaMatricula = matriculaDto.FechaMatricula;
            oMatricula.EstadoMatricula = matriculaDto.EstadoMatricula;
            oMatricula.GradoSolicitado = matriculaDto.GradoSolicitado;
           
           

            // Guarda los cambios en tu almacén de datos o base de datos

            _dbcontext.Matriculas.Update(oMatricula);
            _dbcontext.SaveChanges();

            return Ok();
        }

        private Matricula ObtenerMatriculaPorId(int idMatricula)
        {
            // Lógica para obtener el tutor desde tu almacén de datos o base de datos
            var matricula = _dbcontext.Matriculas.FirstOrDefault(m => m.IdMatricula == idMatricula);

            return matricula;
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


