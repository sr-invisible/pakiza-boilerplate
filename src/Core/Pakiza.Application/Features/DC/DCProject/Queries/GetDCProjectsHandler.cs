namespace Pakiza.Application.Features.DC.DCProject.Queries;

public class GetDCProjectsHandler(IDCProjectService dCProjectService) : IQueryHandler<GetDCProjectsQuery, GetDCProjectsResult>
{
    public async Task<GetDCProjectsResult> Handle(GetDCProjectsQuery request, CancellationToken cancellationToken)
    {
        var result = await dCProjectService.GetAllAsync();
        return new GetDCProjectsResult(result);
    }
}