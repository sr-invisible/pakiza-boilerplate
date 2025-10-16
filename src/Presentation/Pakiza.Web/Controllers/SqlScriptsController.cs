using Pakiza.Application.Features.DC.SQL.Commands;
using Pakiza.Application.Request;

namespace Pakiza.Web.Controllers
{
    public class SqlScriptsController : BaseMvcController
    {
        public SqlScriptsController(IMediator mediator) : base(mediator)
        {
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] SqlScriptRequest dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _mediator.Send(new CreateSqlScriptCommand(dto.Adapt<SqlScriptDTO>()));
            return RedirectToAction(nameof(Index));
        }
    }
}
