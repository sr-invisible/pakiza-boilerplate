namespace Pakiza.Application.Features.DC.DCCompanyInfo.Queries;

public class GetDCCompanyInfosHandler(IDCCompanyInfoService dCCompanyInfoService) : IQueryHandler<GetDCCompanyInfosQuery, GetDCCompanyInfosResult>
{
    public async Task<GetDCCompanyInfosResult> Handle(GetDCCompanyInfosQuery request, CancellationToken cancellationToken)
    {
        var result = await dCCompanyInfoService.GetAllAsync();
        return new GetDCCompanyInfosResult(result);
    }
}