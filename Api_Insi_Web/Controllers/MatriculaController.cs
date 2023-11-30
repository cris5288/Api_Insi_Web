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
                string mensaje = total == 0 ? "No se encontraron matriculas" : total == 1 ? "Se encontró 1 matricula" : $"Se encontraron {total} matriculas";

                return Ok(new { mensaje, Matriculas = listaMatriculaDto });
                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
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

                return Ok( new { mensaje = " Matricula de Estudiante encontrada", Matricula = matriculaDto });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }
        
        [HttpGet]
        [Route("porEstado/{EstadoMatricula}")]
        public ActionResult GetPorEstado(string EstadoMatricula)
        {
            try
            {
                List<Matricula> matriculas = _dbcontext.Matriculas.Where(m => m.EstadoMatricula.ToLower().Contains(EstadoMatricula.ToLower())).ToList();


                if (matriculas.Count == 0)
                {
                    return NotFound("Matricula de Estudiante no encontrada");
                }

                List<MatriculaDto> matriculasDto = new List<MatriculaDto>();
                foreach (Matricula oMatricula in matriculas)
                {
                    MatriculaDto matriculaDto = new MatriculaDto
                    {
                        IdMatricula = oMatricula.IdMatricula,
                        IdEstudiante = oMatricula.IdEstudiante,
                        IdTutor = oMatricula.IdTutor,
                        FechaMatricula = oMatricula.FechaMatricula,
                        EstadoMatricula = oMatricula.EstadoMatricula,
                        GradoSolicitado = oMatricula.GradoSolicitado,
                    };

                    matriculasDto.Add(matriculaDto);
                }
                return Ok(new { mensaje = $"Se encontraron {matriculas.Count} matriculas con el estado '{EstadoMatricula}'", Matriculas = matriculasDto });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }
        
        [HttpGet]
        [Route("porGradoSolicitado/{gradoSolicitado}")]
        public ActionResult GetPorGradoSolicitado(string gradoSolicitado)
        {
            try
            {
               
                List<Matricula> matriculas = _dbcontext.Matriculas.Where(m => m.GradoSolicitado.ToLower().Contains(gradoSolicitado.ToLower())).ToList();

                if (matriculas.Count == 0)
                {
                    return NotFound("Matricula de Estudiante no encontrada");
                }

                List<MatriculaDto> matriculasDto = new List<MatriculaDto>();
                foreach (Matricula oMatricula in matriculas)
                {
                    MatriculaDto matriculaDto = new MatriculaDto
                    {
                        IdMatricula = oMatricula.IdMatricula,
                        IdEstudiante = oMatricula.IdEstudiante,
                        IdTutor = oMatricula.IdTutor,
                        FechaMatricula = oMatricula.FechaMatricula,
                        EstadoMatricula = oMatricula.EstadoMatricula,
                        GradoSolicitado = oMatricula.GradoSolicitado,
                    };

                    matriculasDto.Add(matriculaDto);
                }
                return Ok(new { mensaje = $"Se encontraron {matriculas.Count} matriculas con el grado solicitado '{gradoSolicitado}'", Matriculas = matriculasDto });
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
                return NotFound("Matricula de Estudiante no encontrada");
            }
            try
            {

                oMatricula.FechaMatricula = objeto.FechaMatricula;
                oMatricula.EstadoMatricula = objeto.EstadoMatricula;
                oMatricula.GradoSolicitado = objeto.GradoSolicitado;

                _dbcontext.Matriculas.Update(oMatricula);
                _dbcontext.SaveChanges();

                return Ok( "Matricula de Estudiante editada correctamente");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPatch("Editar/{idMatricula:int}")]
        public ActionResult ActualizarMatricula(int idMatricula, [FromBody] JsonPatchDocument<MatriculaDto> patchDocument)
        {
            var oMatricula = ObtenerMatriculaPorId(idMatricula);

            if (oMatricula == null)
            {
                return NotFound();
            }

            var matriculaDto = new MatriculaDto()
            {
                FechaMatricula = oMatricula.FechaMatricula,
                EstadoMatricula = oMatricula.EstadoMatricula,
                GradoSolicitado = oMatricula.GradoSolicitado,
            };

            patchDocument.ApplyTo(matriculaDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(matriculaDto))
            {
                return BadRequest(ModelState);
            }

            oMatricula.FechaMatricula = matriculaDto.FechaMatricula;
            oMatricula.EstadoMatricula = matriculaDto.EstadoMatricula;
            oMatricula.GradoSolicitado = matriculaDto.GradoSolicitado;
          

            _dbcontext.Matriculas.Update(oMatricula);
            _dbcontext.SaveChanges();

            return Ok("Matricula de Estudiante editada correctamente");
        }

        private Matricula ObtenerMatriculaPorId(int idMatricula)
        {
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


