using Api_Insi_Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Insi_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackupController : ControllerBase
    {
        private readonly BdInsiContext _dbContext; 

        public BackupController(BdInsiContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("backup-completo")]
        public IActionResult BackupCompleto()
        {
            _dbContext.Database.ExecuteSqlRaw("EXEC sp_BackupCompleto");
            return Ok("Backup completo realizado");
        }

        [HttpPost("backup-diferencial")]
        public IActionResult BackupDiferencial()
        {
            _dbContext.Database.ExecuteSqlRaw("EXEC sp_BackupDiferencial");
            return Ok("Backup diferencial realizado");
        }
    }

}

 