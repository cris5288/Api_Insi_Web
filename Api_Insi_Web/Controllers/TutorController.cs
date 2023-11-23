﻿using Microsoft.AspNetCore.Http;
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
        [Route("Editar/{idTutor:int}")]
        public ActionResult Editar(int idTutor, [FromBody] Tutores objeto3)
        {
            Tutores oTutores = _dbcontext.Tutores.Find(idTutor);

            if (oTutores == null)
            {
                return BadRequest("Tutor no encontrado");
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

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Datos de Tutor editados correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }



        [HttpPatch("Editar/{idTutor:int}")]
        public ActionResult ActualizarTutor(int idTutor, [FromBody] JsonPatchDocument<TutoresDto> patchDocument)
        {
            // Obtén el tutor de tu almacén de datos o base de datos
            var oTutor = ObtenerTutorPorId(idTutor);

            if (oTutor == null)
            {
                return NotFound();
            }

            // Crea un objeto TutoresDto para aplicar los cambios parciales
            var tutorDto = new TutoresDto()
            {
               
                Nombre = oTutor.Nombre,
                Apellido = oTutor.Apellido,
                Direccion = oTutor.Direccion,
                Telefono = oTutor.Telefono,
                RelacionConEstudiante = oTutor.RelacionConEstudiante
            };

            // Aplica los cambios parciales al tutorDto utilizando el patchDocument
            patchDocument.ApplyTo(tutorDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(tutorDto))
            {
                return BadRequest(ModelState);
            }

            // Aplica los cambios al tutor original
            oTutor.Nombre = tutorDto.Nombre;
            oTutor.Apellido = tutorDto.Apellido;
            oTutor.Direccion = tutorDto.Direccion;
            oTutor.Telefono = tutorDto.Telefono;
            oTutor.RelacionConEstudiante = tutorDto.RelacionConEstudiante;

            // Guarda los cambios en tu almacén de datos o base de datos

            _dbcontext.Tutores.Update(oTutor);
            _dbcontext.SaveChanges();

            return Ok();
        }

        private Tutores ObtenerTutorPorId(int idTutor)
        {
            // Lógica para obtener el tutor desde tu almacén de datos o base de datos
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
