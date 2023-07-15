using Microsoft.AspNetCore.Mvc;
using Noon.Api.Errors;

namespace Noon.Api.Controllers
{
    [Route("errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        public ActionResult Error(int code)
        {
            return NotFound(new ApiResponse(code, "Not Found End Point"));
        }
    }
}
