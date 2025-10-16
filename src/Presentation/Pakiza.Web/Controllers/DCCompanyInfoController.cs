using Pakiza.Application.Features.DC.DCCompanyInfo.Commands;
using Pakiza.Application.Features.DC.DCCompanyInfo.Queries;

namespace Pakiza.Web.Controllers;

[Authorize]
public class DCCompanyInfoController : BaseMvcController
{
    public DCCompanyInfoController(IMediator mediator) : base(mediator) { }

    public async Task<IActionResult> Index()
    {
        await Task.CompletedTask;
        ViewBag.StatusSelectList = RkFn_IsActive();
        return View();
    }

    public async Task<JsonResult> GetCompanyList()
    {
        string tableData = string.Empty;
        var result = await _mediator.Send(new GetDCCompanyInfosQuery());

        foreach (var obj in result.DCCompanyInfo)
        {
            string actionOpt = "<i onclick='OpenPopupModel(\"" + obj.Id + "\");' title='Edit' class='md-icon md-24 material-icons md-color-orange-400'>&#xE254;</i>";

            tableData += "<tr>"
            + "<td>" + actionOpt + "</td>"
            + "<td>" + obj.CompanyName + "</td>"
            + "<td>" + obj.Address + "</td>"
            + "<td>" + obj.ContactPerson + "</td>"
            + "<td>" + obj.Phone + "</td>"
            + "<td>" + obj.Email + "</td>"
            + "<td>" + obj.WebUrl + "</td>"
            + "</tr>";
        }
          
        string tHead = "<tr><th>&nabla;</th><th>Company Name</th><th>Address</th><th>Contact Person</th><th>Phone</th><th>Email</th><th>Web URL</th></tr>";
        tableData = "<div class='rk_dtButton2'></div><table id='rk_dtInfo2' class='uk-table uk-table-hover uk-table-condensed' cellspacing='0' width='100%'>"
            + "<thead>" + tHead + "</thead><tfoot style='display:none;'>" + tHead + "</tfoot><tbody id='tableBody'>" + tableData + "</tbody></table>";

        return Json(new { Success = true, TableData = tableData });
    }

    [HttpPost]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> UpsertCompanyInfo(DCCompanyInfoUpsertRequest dto)
    {
        if (!ModelState.IsValid)
            RedirectToAction(nameof(Index));
        var obj = dto.Adapt<DCCompanyInfoDTO>();

        if (dto.Id == null || dto.Id == default)
        {
            await _mediator.Send(new CreateDCCompanyInfoCommand(obj));
        }
        else
        {
            await _mediator.Send(new UpdateDCCompanyInfoCommand(obj));
        }
        return Json(Result<string>.Success("Success saving data."));
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var data = await _mediator.Send(new GetDCCompanyInfoByIdQuery(id));
        if (data == null)
            return NotFound();

        return Json(data);
    }

    public List<SelectListItem> RkFn_IsActive(int Type = 0, string On = "Active", string Off = "Inactive")
    {
        List<SelectListItem> Items = new List<SelectListItem>();
        if (Type == 3)
        {
            Items.Add(new SelectListItem { Value = "True", Text = On });
            Items.Add(new SelectListItem { Value = "False", Text = Off });
        }
        else if (Type == 2)
        {
            Items.Add(new SelectListItem { Value = On, Text = On });
            Items.Add(new SelectListItem { Value = Off, Text = Off });
        }
        else if (Type == 1)
        {
            Items.Add(new SelectListItem { Value = "1", Text = On });
            Items.Add(new SelectListItem { Value = "0", Text = Off });
        }
        else
        {
            Items.Add(new SelectListItem { Value = "Active", Text = On });
            Items.Add(new SelectListItem { Value = "Inactive", Text = Off });
        }
        return Items;
    }
}