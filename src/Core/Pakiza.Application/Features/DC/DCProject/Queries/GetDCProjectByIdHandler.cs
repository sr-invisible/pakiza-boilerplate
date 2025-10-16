namespace Pakiza.Application.Features.DC.DCProject.Queries;

public class GetDCProjectByIdHandler(IDCProjectService dCProjectService) : IQueryHandler<GetDCProjectByIdQuery, GetDCProjectByIdResult>
{
    public async Task<GetDCProjectByIdResult> Handle(GetDCProjectByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await dCProjectService.GetByIdAsync(query.Id);
        return new GetDCProjectByIdResult(result);
    }
}