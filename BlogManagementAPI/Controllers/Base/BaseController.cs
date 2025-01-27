using Microsoft.AspNetCore.Mvc;

namespace BlogManagementAPI.Controllers.Base
{
    public abstract class BaseController : ControllerBase
    {
        protected readonly ILogger _logger;

        protected BaseController(ILogger logger)
        {
            _logger = logger;
        }

        protected IActionResult HandleException(Exception ex, string message = "An error occurred")
        {
            _logger.LogError(ex, message);
            return StatusCode(500, new { Error = message });
        }
    }
}
