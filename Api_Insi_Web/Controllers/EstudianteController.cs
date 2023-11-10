using Api_Insi_Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

            public IActionResult Lista()
            {
                List<Estudiante> lista = new List<Estudiante>();

                try
                {
                lista = _dbcontext.Estudiantes./*Include(t => t.oTutor).*/ToList();

                    return StatusCode(StatusCodes.Status200OK, new { mensaje = "Lista de estudiantes", response = lista });

                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
                }

            }

            [HttpGet]
            [Route("Obtener/{idEstudiante:int}")]
            public IActionResult Obtener(int idEstudiante)
            {
                try
                {
                    Estudiante oEstudiante = _dbcontext.Estudiantes.Find(idEstudiante);

                    if (oEstudiante == null)
                    {
                        return BadRequest("Estudiante no encontrado");
                    }

                    return StatusCode(StatusCodes.Status200OK, new { mensaje = "Estudiante encontrado", response = oEstudiante });
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
                }
            }

        //[HttpPost]
        //[Route("Guardar")]
        //public IActionResult Guardar([FromBody] Estudiante objeto)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid) 
        //        {
        //            _dbcontext.Estudiantes.Add(objeto);
        //            _dbcontext.SaveChanges();

        //            int idTutorRecienAgregado = objeto.oTutor.IdTutor;

        //            return Ok(new { mensaje = "Estudiante guardado correctamente" });
        //        }
        //        else
        //        {
        //            return BadRequest(new { mensaje = "Los datos no son válidos. Por favor, revise que los datos sean correctos." });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Ocurrió un error al guardar el estudiante en la base de datos. Por favor, inténtelo nuevamente." });
        //    }
        //}
        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Estudiante objeto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Lógica para asignar automáticamente un tutor
                    Tutores tutor = _dbcontext.Tutores.OrderByDescending(t => t.IdTutor).FirstOrDefault();
                    objeto.oTutor = tutor;

                    _dbcontext.Estudiantes.Add(objeto);
                    _dbcontext.SaveChanges();

                    int idTutorRecienAgregado = objeto.oTutor.IdTutor;

                    return Ok(new { mensaje = "Estudiante guardado correctamente" });
                }
                else
                {
                    return BadRequest(new { mensaje = "Los datos no son válidos. Por favor, revise que los datos sean correctos." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Ocurrió un error al guardar el estudiante en la base de datos. Por favor, inténtelo nuevamente." });
            }
        }





        [HttpPut]
            [Route("Editar")]
            public IActionResult Editar([FromBody] Estudiante objeto2)
            {

                Estudiante oEstudiante = _dbcontext.Estudiantes.Find(objeto2.IdEstudiante);

                if (oEstudiante == null)
                {
                    return BadRequest("Estudiante no encontrado");
                }
                try
                {
                   
                    //oEstudiante.IdTutor = objeto2.IdTutor is null ? oEstudiante.IdTutor : objeto2.IdTutor;
                    oEstudiante.Nombre = objeto2.Nombre is null ? oEstudiante.Nombre : objeto2.Nombre;
                    oEstudiante.Apellido = objeto2.Apellido is null ? oEstudiante.Apellido : objeto2.Apellido;
                    oEstudiante.FechaNacimiento = objeto2.FechaNacimiento is null ? oEstudiante.FechaNacimiento : objeto2.FechaNacimiento;
                    oEstudiante.LugarNacimiento = objeto2.LugarNacimiento is null ? oEstudiante.LugarNacimiento : objeto2.LugarNacimiento;
                    oEstudiante.ZonaRecidencial = objeto2.ZonaRecidencial is null ? oEstudiante.ZonaRecidencial : objeto2.ZonaRecidencial;
                    oEstudiante.PartidaNacimiento = objeto2.PartidaNacimiento is null ? oEstudiante.PartidaNacimiento : objeto2.PartidaNacimiento;
                    oEstudiante.Edad = objeto2.Edad is null ? oEstudiante.Edad : objeto2.Edad;
                    oEstudiante.Genero = objeto2.Genero is null ? oEstudiante.Genero : objeto2.Genero;
                    oEstudiante.Direccion = objeto2.Direccion is null ? oEstudiante.Direccion : objeto2.Direccion;
                    oEstudiante.Telefono = objeto2.Telefono is null ? oEstudiante.Telefono : objeto2.Telefono;
                    oEstudiante.UltimoGradoAprobado = objeto2.UltimoGradoAprobado is null ? oEstudiante.UltimoGradoAprobado : objeto2.UltimoGradoAprobado;
                    oEstudiante.EstaRepitiendoGrado = objeto2.EstaRepitiendoGrado is null ? oEstudiante.EstaRepitiendoGrado : objeto2.EstaRepitiendoGrado;

                    _dbcontext.Estudiantes.Update(oEstudiante);
                    _dbcontext.SaveChanges();

                    return StatusCode(StatusCodes.Status200OK, new { mensaje = "Datos de Estudiante editados correctamente" });
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
                }

            }


            [HttpDelete]
            [Route("Eliminar/{idEstudiante:int}")]
            public IActionResult Eliminar(int idEstudiante)
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
                    return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
                }
            }


        }
    }


