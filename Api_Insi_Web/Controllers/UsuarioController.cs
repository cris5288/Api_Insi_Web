using Api_Insi_Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Insi_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        public readonly BdInsiContext _dbcontext;

        public UsuarioController(BdInsiContext _context)
        {
            _dbcontext = _context;
        }

        //[HttpPost("Cambiar de Usuario")]
        //public IActionResult ChangeUser(string username)
        //{
        //    // Obtener una instancia del contexto de la base de datos
        //    var dbContext = new BdInsiContext();

        //    // Ejecutar el comando EXECUTE AS USER utilizando el nombre de usuario proporcionado
        //    dbContext.Database.ExecuteSqlRaw($"EXECUTE AS USER = '{username}'");

        //    // Realizar cualquier otra lógica que necesites con el nuevo usuario

        //    // Devolver una respuesta exitosa
        //    return Ok("Se cambio de Usuario correctamente");
        //}




    }
}
