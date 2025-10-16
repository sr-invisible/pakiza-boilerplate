namespace Pakiza.Application.Features.DC.DCCompanyInfo.Queries;

public class GetDCCompanyInfoByIdHandler(IDCCompanyInfoService dCCompanyInfoService) : IQueryHandler<GetDCCompanyInfoByIdQuery, GetDCCompanyInfoByIdResult>
{
    public async Task<GetDCCompanyInfoByIdResult> Handle(GetDCCompanyInfoByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await dCCompanyInfoService.GetByIdAsync(query.Id);
        return new GetDCCompanyInfoByIdResult(result);
    }
}