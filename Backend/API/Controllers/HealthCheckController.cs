using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        private readonly ProjectDBContext _context;

        public HealthCheckController(ProjectDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult CheckDatabaseConnection()
        {
            try
            {
                // Verificar si la base de datos responde
                _context.Database.CanConnect();
                return Ok(new { status = "Healthy", message = "Database connection successful!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = "Unhealthy", error = ex.Message });
            }
        }
    }
}
