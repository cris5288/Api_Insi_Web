using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api_Insi_Web.Models;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;


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
                string mensaje = total == 0 ? "No se encontraron tutores" : total == 1 ? "Se encontró 1 tutor" : $"Se encontraron {total} tutores";

                return Ok(new { mensaje,  Tutores = listaTutoresDto });
            }
           
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
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
                    return NotFound("Tutor no encontrado");
                }

                return Ok( new { mensaje = "Tutor encontrado", Tutor = oTutores });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpGet]
        [Route("buscarTutorPorNombre/{nombre}")]
        public ActionResult getBuscarTutorPorNombre(string nombre)
        {
            try
            {
                // Realizar la búsqueda en la base de datos por el nombre del tutor
                List<Tutores> tutores = _dbcontext.Tutores.Where(t => t.Nombre.ToLower().Contains(nombre.ToLower())).ToList();

                if (tutores.Count == 0)
                {
                    return NotFound("Tutor no encontrado");
                }

                List<TutoresDto> tutoresDto = new List<TutoresDto>();
                foreach (Tutores tutor in tutores)
                {
                    TutoresDto tutorDto = new TutoresDto
                    {
                        IdTutor = tutor.IdTutor,
                        Nombre = tutor.Nombre,
                        Apellido = tutor.Apellido,
                        Direccion = tutor.Direccion,
                        Telefono = tutor.Telefono,
                        RelacionConEstudiante = tutor.RelacionConEstudiante
                    };

                    tutoresDto.Add(tutorDto);
                }

                return Ok(new { mensaje = $"Se encontraron {tutores.Count} tutores con el nombre '{nombre}'", Tutores = tutoresDto });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpGet]
        [Route("buscarTutorPorApellido/{apellido}")]
        public ActionResult getBuscarTutorPorApellido(string apellido)
        {
            try
            {
               
                List<Tutores> tutores = _dbcontext.Tutores.Where(t => t.Apellido.ToLower().Contains(apellido.ToLower())).ToList();

                if (tutores.Count == 0)
                {
                    return NotFound("Tutor no encontrado");
                }

                List<TutoresDto> tutoresDto = new List<TutoresDto>();
                foreach (Tutores tutor in tutores)
                {
                    TutoresDto tutorDto = new TutoresDto
                    {
                        IdTutor = tutor.IdTutor,
                        Nombre = tutor.Nombre,
                        Apellido = tutor.Apellido,
                        Direccion = tutor.Direccion,
                        Telefono = tutor.Telefono,
                        RelacionConEstudiante = tutor.RelacionConEstudiante
                    };

                    tutoresDto.Add(tutorDto);
                }
                return Ok(new { mensaje = $"Se encontraron {tutores.Count} tutores con el apellido '{apellido}'", Tutores = tutoresDto });

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

                Tutores objeto = new Tutores
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
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("Editar/{idTutor:int}")]
        public ActionResult Editar(int idTutor, [FromBody] Tutores objeto3)
        {
            Tutores oTutores = _dbcontext.Tutores.Find(idTutor);

            if (oTutores == null)
            {
                return NotFound("Tutor no encontrado");
            }

            try
            {
                oTutores.Nombre = objeto3.Nombre;
                oTutores.Apellido = objeto3.Apellido;
                oTutores.Direccion = objeto3.Direccion;
                oTutores.Telefono = objeto3.Telefono;
                oTutores.RelacionConEstudiante = objeto3.RelacionConEstudiante;

                _dbcontext.Tutores.Update(oTutores);
                _dbcontext.SaveChanges();

                return Ok( new { mensaje = "Datos de Tutor editados correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }



        [HttpPatch("Editar/{idTutor:int}")]
        public ActionResult ActualizarTutor(int idTutor, [FromBody] JsonPatchDocument<TutoresDto> patchDocument)
        {
            var oTutor = ObtenerTutorPorId(idTutor);

            if (oTutor == null)
            {
                return NotFound();
            }

            var tutorDto = new TutoresDto()
            {

                Nombre = oTutor.Nombre,
                Apellido = oTutor.Apellido,
                Direccion = oTutor.Direccion,
                Telefono = oTutor.Telefono,
                RelacionConEstudiante = oTutor.RelacionConEstudiante
            };


            patchDocument.ApplyTo(tutorDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(tutorDto))
            {
                return BadRequest(ModelState);
            }

            oTutor.Nombre = tutorDto.Nombre;
            oTutor.Apellido = tutorDto.Apellido;
            oTutor.Direccion = tutorDto.Direccion;
            oTutor.Telefono = tutorDto.Telefono;
            oTutor.RelacionConEstudiante = tutorDto.RelacionConEstudiante;

            _dbcontext.Tutores.Update(oTutor);
            _dbcontext.SaveChanges();

            return Ok("Datos de Tutor editados correctamente");
        }

        private Tutores ObtenerTutorPorId(int idTutor)
        {
            var tutor = _dbcontext.Tutores.FirstOrDefault(t => t.IdTutor == idTutor);

            return tutor;
        }

        [HttpDelete]
        [Route("Eliminar/{idTutores:int}")]
        public ActionResult Eliminar(int idTutores)
        {
            Tutores oTutores = _dbcontext.Tutores.Find(idTutores);
            if (oTutores == null)
            {
                return NotFound("Tutor no encontrado");
            }

            try
            {
                _dbcontext.Tutores.Remove(oTutores);
                _dbcontext.SaveChanges();

                return Ok( new { mensaje = "Tutor eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }
    }
}
