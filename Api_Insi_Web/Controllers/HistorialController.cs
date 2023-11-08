using Api_Insi_Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace INSI_WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialController : ControllerBase
    {

        public readonly BdInsiContext _dbcontext;

        public HistorialController(BdInsiContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("Lista")]

        public IActionResult Lista()
        {
            List<Historial> lista = new List<Historial>();

            try
            {

                lista = _dbcontext.Historials.ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Lista de Historial de Matricula", response = lista });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
            }

        }
    }
}
