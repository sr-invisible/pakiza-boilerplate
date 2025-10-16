

using Asp.Versioning;

namespace Pakiza.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion(1)]
    public class UserController(IMediator mediator) : BaseApiController(mediator)
    {
    }
}
