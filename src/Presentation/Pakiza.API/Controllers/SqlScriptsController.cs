
using Mapster;
using Pakiza.Application.DTOs.DC;
using Pakiza.Application.Features.DC.SQL.Commands;
using Pakiza.Application.Features.DC.SQL.Queries;
using Pakiza.Application.Request;

namespace Pakiza.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class SqlScriptsController(IMediator mediator) : BaseApiController(mediator)
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] SqlScriptRequest dto)
        {
            var sqlScriptDTO = new SqlScriptDTO
            {
                Name = dto.Name,
                ScriptFile = dto.ScriptFile,
                Script = dto.Script,
                Version = dto.Version,
                Remarks = dto.Remarks

            };
            var result = await _mediator.Send(new CreateSqlScriptCommand(sqlScriptDTO));
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _mediator.Send(new GetSqlScriptByIdQuery(id));
            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProductRequest dto)
        {
            var updatedProduct = dto.Adapt<ProductDTO>();
            updatedProduct.Id = id;
            var result = await _mediator.Send(new UpdateSqlScriptCommand(updatedProduct));
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteSqlScriptCommand(id));
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetSqlScriptsQuery());
            return Ok(result);
        }

        [HttpGet("{id}/download")]
        public async Task<IActionResult> DownloadScript(Guid id)
        {
            try
            {
                var result = await _mediator.Send(new GetSqlScriptFileByIdQuery(id));
                return File(result.SqlScriptFileResponse.FileData, "application/sql", result.SqlScriptFileResponse.FileName);
            }
            catch (FileNotFoundException)
            {
                return NotFound("Script file not found.");
            }
        }
    }
}
