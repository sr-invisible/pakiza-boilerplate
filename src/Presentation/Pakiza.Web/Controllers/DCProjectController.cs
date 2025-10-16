using Pakiza.Application.Features.DC.DCProject.Commands;
using Pakiza.Application.Features.DC.DCProject.Queries;

namespace Pakiza.Web.Controllers;

[Authorize]
public class DCProjectController : BaseMvcController
{
    public DCProjectController(IMediator mediator) : base(mediator) { }

    public async Task<IActionResult> Index()
    {
        var statusList = new List<SelectListItem>
        {
            new SelectListItem { Value = "Active", Text = "Active" },
            new SelectListItem { Value = "Inactive", Text = "Inactive" },
        };
        ViewBag.StatusSelectList = statusList;
        await Task.CompletedTask;
        return View();
    }

    public async Task<JsonResult> GetProjectList()
    {
        string tableData = string.Empty;
        var result = await _mediator.Send(new GetDCProjectsQuery());

        foreach (var obj in result.DCProject)
        {
            string actionOpt = "<i onclick='OpenPopupModel(\"" + obj.Id + "\");' title='Edit' class='md-icon md-24 material-icons md-color-orange-400'>&#xE254;</i>";

            tableData += "<tr>"
            + "<td>" + actionOpt + "</td>"
            + "<td>" + obj.ProjectName + "</td>"
            + "<td>" + obj.Status + "</td>" 
            + "</tr>";
        }

        string tHead = "<tr><th>&nabla;</th><th>Project Name</th><th>Status</th></tr>";
        tableData = "<div class='rk_dtButton2'></div><table id='rk_dtInfo2' class='uk-table uk-table-hover uk-table-condensed' cellspacing='0' width='100%'>"
            + "<thead>" + tHead + "</thead><tfoot style='display:none;'>" + tHead + "</tfoot><tbody id='tableBody'>" + tableData + "</tbody></table>";

        return Json(new { Success = true, TableData = tableData });
    }

    [HttpPost]
    public async Task<IActionResult> UpsertProject(DCProjectUpsertRequest dto)
    {
        if (!ModelState.IsValid)
            RedirectToAction(nameof(Index));

        var obj = dto.Adapt<DCProjectDTO>();

        if (dto.Id == null || dto.Id == default)
        {
            await _mediator.Send(new CreateDCProjectCommand(obj));
        }
        else
        {
            await _mediator.Send(new UpdateDCProjectCommand(obj));
        }
        return Json(Result<string>.Success("Success saving data."));
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var data = await _mediator.Send(new GetDCProjectByIdQuery(id)); // Use the new query for DCProject
        if (data == null)
            return NotFound();

        return Json(data);
    }
}