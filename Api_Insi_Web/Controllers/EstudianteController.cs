using Api_Insi_Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;

namespace Api_Insi_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {
       
            public readonly BdInsiContext _dbcontext;

            public EstudianteController(BdInsiContext _context)
            {
                _dbcontext = _context;
            }

        [HttpGet]
        [Route("Lista")]
        public ActionResult Lista()
        {
            List<EstudianteDto> listaEstudiantesDto = new List<EstudianteDto>();

            try
            {
                int total = _dbcontext.Estudiantes.Count();
                var estudiantes = _dbcontext.Estudiantes.ToList();

                foreach (var estudiante in estudiantes)
                {
                    var estudianteDto = new EstudianteDto
                    {
                        IdEstudiante = estudiante.IdEstudiante,
                        IdTutor = estudiante.IdTutor,
                        Nombre = estudiante.Nombre,
                        Apellido = estudiante.Apellido,
                        FechaNacimiento = estudiante.FechaNacimiento,
                        LugarNacimiento = estudiante.LugarNacimiento,
                        ZonaRecidencial = estudiante.ZonaRecidencial,
                        PartidaNacimiento = estudiante.PartidaNacimiento,
                        Edad = estudiante.Edad,
                        Genero = estudiante.Genero,
                        Direccion = estudiante.Direccion,
                        Telefono = estudiante.Telefono,
                        UltimoGradoAprobado = estudiante.UltimoGradoAprobado,
                        EstaRepitiendoGrado = estudiante.EstaRepitiendoGrado
                    };

                    listaEstudiantesDto.Add(estudianteDto);
                }

                return Ok(new { mensaje = "Lista de estudiantes", TotalEstudiantes = total, lista = listaEstudiantesDto });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, response = listaEstudiantesDto });
            }
        }

        [HttpGet]
        [Route("Obtener/{idEstudiante:int}")]
        public ActionResult Obtener(int idEstudiante)
        {
            try
            {
                Estudiante oEstudiante = _dbcontext.Estudiantes.Find(idEstudiante);

                if (oEstudiante == null)
                {
                    return NotFound("Estudiante no encontrado");
                }

                EstudianteDto estudianteDto = new EstudianteDto
                {
                    IdEstudiante = oEstudiante.IdEstudiante,
                    IdTutor = oEstudiante.IdTutor,
                    Nombre = oEstudiante.Nombre,
                    Apellido = oEstudiante.Apellido,
                    FechaNacimiento = oEstudiante.FechaNacimiento,
                    LugarNacimiento = oEstudiante.LugarNacimiento,
                    ZonaRecidencial = oEstudiante.ZonaRecidencial,
                    PartidaNacimiento = oEstudiante.PartidaNacimiento,
                    Edad = oEstudiante.Edad,
                    Genero = oEstudiante.Genero,
                    Direccion = oEstudiante.Direccion,
                    Telefono = oEstudiante.Telefono,
                    UltimoGradoAprobado = oEstudiante.UltimoGradoAprobado,
                    EstaRepitiendoGrado = oEstudiante.EstaRepitiendoGrado,
                    
                };

                return Ok(new { mensaje = "Estudiante encontrado", response = estudianteDto });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

      
        [HttpPost]
[Route("Guardar")]
public ActionResult Guardar([FromBody] EstudianteDto objeto)
{
    try
    {
        Tutores tutor = _dbcontext.Tutores.OrderByDescending(t => t.IdTutor).FirstOrDefault();
        objeto.oTutor = tutor;

        Estudiante estudiante = new Estudiante
        {
            IdEstudiante = objeto.IdEstudiante,
            IdTutor = objeto.IdTutor,
            Nombre = objeto.Nombre,
            Apellido = objeto.Apellido,
            FechaNacimiento = objeto.FechaNacimiento,
            LugarNacimiento = objeto.LugarNacimiento,
            ZonaRecidencial = objeto.ZonaRecidencial,
            PartidaNacimiento = objeto.PartidaNacimiento,
            Edad = objeto.Edad,
            Genero = objeto.Genero,
            Direccion = objeto.Direccion,
            Telefono = objeto.Telefono,
            UltimoGradoAprobado = objeto.UltimoGradoAprobado,
            EstaRepitiendoGrado = objeto.EstaRepitiendoGrado,
            oTutor = objeto.oTutor
        };

        _dbcontext.Estudiantes.Add(estudiante);
        _dbcontext.SaveChanges();

                return CreatedAtAction(nameof(Guardar), new { id = objeto.IdEstudiante }, new { mensaje = "Estudiante guardado correctamente." });
            }
    catch (Exception ex)
    {
        return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Ocurrió un error al guardar el estudiante en la base de datos. Por favor, inténtelo nuevamente." });
    }
}

        [HttpPut]
            [Route("Editar/{idEstudiante:int}")]
            public ActionResult Editar(int idEstudiante, [FromBody] Estudiante objeto2)
            {

                Estudiante oEstudiante = _dbcontext.Estudiantes.Find(idEstudiante);

                if (oEstudiante == null)
                {
                    return BadRequest("Estudiante no encontrado");
                }
                try
                {

                    oEstudiante.Nombre = objeto2.Nombre;
                    oEstudiante.Apellido = objeto2.Apellido;
                    oEstudiante.FechaNacimiento = objeto2.FechaNacimiento;
                    oEstudiante.LugarNacimiento = objeto2.LugarNacimiento;
                    oEstudiante.ZonaRecidencial = objeto2.ZonaRecidencial;
                    oEstudiante.PartidaNacimiento = objeto2.PartidaNacimiento;
                    oEstudiante.Edad = objeto2.Edad;
                    oEstudiante.Genero = objeto2.Genero ;
                    oEstudiante.Direccion = objeto2.Direccion;
                    oEstudiante.Telefono = objeto2.Telefono;
                    oEstudiante.UltimoGradoAprobado = objeto2.UltimoGradoAprobado;
                    oEstudiante.EstaRepitiendoGrado = objeto2.EstaRepitiendoGrado;

                    _dbcontext.Estudiantes.Update(oEstudiante);
                    _dbcontext.SaveChanges();

                    return StatusCode(StatusCodes.Status200OK, new { mensaje = "Datos de Estudiante editados correctamente" });
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
                }

            }

        [HttpPatch("Editar/{idEstudiante:int}")]
        public ActionResult ActualizarEstudiante(int idEstudiante, [FromBody] JsonPatchDocument<EstudianteDto> patchDocument)
        {
            // Obtén el estudiante de tu almacén de datos o base de datos
            var oEstudiante = ObtenerEstudiantePorId(idEstudiante);

            if (oEstudiante == null)
            {
                return NotFound();
            }

            // Crea un objeto EstudianteDto y asigna los valores del estudiante original
            var estudianteDto = new EstudianteDto()
            {
                Nombre = oEstudiante.Nombre,
                Apellido = oEstudiante.Apellido,
                FechaNacimiento = oEstudiante.FechaNacimiento,
                LugarNacimiento = oEstudiante.LugarNacimiento,
                ZonaRecidencial = oEstudiante.ZonaRecidencial,
                PartidaNacimiento = oEstudiante.PartidaNacimiento,
                Edad = oEstudiante.Edad,
                Genero = oEstudiante.Genero,
                Direccion = oEstudiante.Direccion,
                Telefono = oEstudiante.Telefono,
                UltimoGradoAprobado = oEstudiante.UltimoGradoAprobado,
                EstaRepitiendoGrado = oEstudiante.EstaRepitiendoGrado,
            };

            // Aplica los cambios parciales al estudianteDto utilizando el patchDocument
            patchDocument.ApplyTo(estudianteDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(estudianteDto))
            {
                return BadRequest(ModelState);
            }

            // Aplica los cambios al estudiante original
            oEstudiante.Nombre = estudianteDto.Nombre;
            oEstudiante.Apellido = estudianteDto.Apellido;
            oEstudiante.FechaNacimiento = estudianteDto.FechaNacimiento;
            oEstudiante.LugarNacimiento = estudianteDto.LugarNacimiento;
            oEstudiante.ZonaRecidencial = estudianteDto.ZonaRecidencial;
            oEstudiante.PartidaNacimiento = estudianteDto.PartidaNacimiento;
            oEstudiante.Edad = estudianteDto.Edad;
            oEstudiante.Genero = estudianteDto.Genero;
            oEstudiante.Direccion = estudianteDto.Direccion;
            oEstudiante.Telefono = estudianteDto.Telefono;
            oEstudiante.UltimoGradoAprobado = estudianteDto.UltimoGradoAprobado;
            oEstudiante.EstaRepitiendoGrado = estudianteDto.EstaRepitiendoGrado;

            // Guarda los cambios en tu almacén de datos o base de datos
            _dbcontext.Estudiantes.Update(oEstudiante);
            _dbcontext.SaveChanges();

            return Ok();

        }
        private Estudiante ObtenerEstudiantePorId(int idEstudiante)
        {
            // Lógica para obtener el tutor desde tu almacén de datos o base de datos
            var estudiante = _dbcontext.Estudiantes.FirstOrDefault(e => e.IdEstudiante == idEstudiante);

            return estudiante;
        }

        [HttpDelete]
            [Route("Eliminar/{idEstudiante:int}")]
            public ActionResult Eliminar(int idEstudiante)
            {
                Estudiante oEstudiante = _dbcontext.Estudiantes.Find(idEstudiante);
                if (oEstudiante == null)
                {
                    return BadRequest("Estudiante no encontrado");
                }

                try
                {
                    _dbcontext.Estudiantes.Remove(oEstudiante);
                    _dbcontext.SaveChanges();

                    return StatusCode(StatusCodes.Status200OK, new { mensaje = "Estudiante eliminado correctamente" });
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
                }
            }


        }
    }


