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
                string mensaje = total == 0 ? "No se encontraron estudiantes" : total == 1 ? "Se encontró 1 estudiante" : $"Se encontraron {total} estudiantes";

                return Ok(new { mensaje, estudiantes = listaEstudiantesDto });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
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

                return Ok(new { mensaje = "Estudiante encontrado", Estudiante = estudianteDto });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }
        [HttpGet]
        [Route("buscarEstudiantePorNombre/{nombre}")]
        public ActionResult getBuscarEstudiantePorNombre(string nombre)
        {
            try
            {
               
                List<Estudiante> estudiante = _dbcontext.Estudiantes.Where(t => t.Nombre.ToLower().Contains(nombre.ToLower())).ToList();

                if (estudiante.Count == 0)
                {
                    return NotFound("Estudiante no encontrado");
                }

                List<EstudianteDto> estudianteDto = new List<EstudianteDto>();
                foreach (Estudiante oEstudiante in estudiante)
                {
                    EstudianteDto estudianteDto1 = new EstudianteDto
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

                    estudianteDto.Add(estudianteDto1);
                }

                return Ok(new { mensaje = $"Se encontraron {estudiante.Count} estudiantes con el nombre '{nombre}'", Estudiantes = estudianteDto });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpGet]
        [Route("buscarEstudiantePorApellido/{apellido}")]
        public ActionResult getBuscarEstudiantePorApellido(string apellido)
        {
            try
            {
               
                List<Estudiante> estudiante = _dbcontext.Estudiantes.Where(t => t.Apellido.ToLower().Contains(apellido.ToLower())).ToList();

                if (estudiante.Count == 0)
                {
                    return NotFound("Estudiante no encontrado");
                }

                List<EstudianteDto> estudianteDto = new List<EstudianteDto>();
                foreach (Estudiante oEstudiante in estudiante)
                {
                    EstudianteDto estudianteDto1 = new EstudianteDto
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

                    estudianteDto.Add(estudianteDto1);
                }
                return Ok(new { mensaje = $"Se encontraron {estudiante.Count} estudiantes con el apellido '{apellido}'", Estudiantes = estudianteDto });
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
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje =ex.Message });
            }
        }

        [HttpPut]
        [Route("Editar/{idEstudiante:int}")]
        public ActionResult Editar(int idEstudiante, [FromBody] Estudiante objeto2)
        {

            Estudiante oEstudiante = _dbcontext.Estudiantes.Find(idEstudiante);

            if (oEstudiante == null)
            {
                return NotFound("Estudiante no encontrado");
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
                oEstudiante.Genero = objeto2.Genero;
                oEstudiante.Direccion = objeto2.Direccion;
                oEstudiante.Telefono = objeto2.Telefono;
                oEstudiante.UltimoGradoAprobado = objeto2.UltimoGradoAprobado;
                oEstudiante.EstaRepitiendoGrado = objeto2.EstaRepitiendoGrado;

                _dbcontext.Estudiantes.Update(oEstudiante);
                _dbcontext.SaveChanges();

                return Ok( "Datos de Estudiante editados correctamente" );
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }

        }

        [HttpPatch("Editar/{idEstudiante:int}")]
        public ActionResult ActualizarEstudiante(int idEstudiante, [FromBody] JsonPatchDocument<EstudianteDto> patchDocument)
        {          
            var oEstudiante = ObtenerEstudiantePorId(idEstudiante);

            if (oEstudiante == null)
            {
                return NotFound();
            }           
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
           
            patchDocument.ApplyTo(estudianteDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(estudianteDto))
            {
                return BadRequest(ModelState);
            }
           
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
           
            _dbcontext.Estudiantes.Update(oEstudiante);
            _dbcontext.SaveChanges();

            return Ok( "Datos de Estudiante editados correctamente");

        }
        private Estudiante ObtenerEstudiantePorId(int idEstudiante)
        {            
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
                    return NotFound("Estudiante no encontrado");
                }

                try
                {
                    _dbcontext.Estudiantes.Remove(oEstudiante);
                    _dbcontext.SaveChanges();

                    return Ok( "Estudiante eliminado correctamente" );
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
                }
            }


        }
    }


