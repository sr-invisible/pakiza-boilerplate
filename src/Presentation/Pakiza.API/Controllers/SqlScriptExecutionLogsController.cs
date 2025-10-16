
using Mapster;
using Pakiza.Application.DTOs.DC;
using Pakiza.Application.Features.DC.SqlScriptExecutionLog.Commands;
using Pakiza.Application.Features.DC.SqlScriptExecutionLog.Queries;
using Pakiza.Application.Features.DC.SqlScriptExecutionLog.Request;
namespace Pakiza.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class SqlScriptExecutionLogsController(IMediator mediator) : BaseApiController(mediator)
    {
        [HttpPost]
        public async Task<IActionResult> Create(SqlScriptExecutionLogRequest dto)
        {
            var result = await _mediator.Send(new CreateSqlScriptExecutionLogCommand(dto.Adapt<SqlScriptExecutionLogDTO>()));
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _mediator.Send(new GetSqlScriptExecutionLogByIdQuery(id));
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetSqlScriptExecutionLogsQuery());
            return Ok(result);
        }
    }
}
