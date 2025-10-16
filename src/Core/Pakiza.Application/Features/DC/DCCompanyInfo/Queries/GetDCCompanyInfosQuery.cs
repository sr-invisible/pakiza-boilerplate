namespace Pakiza.Application.Features.DC.DCCompanyInfo.Queries;

public record GetDCCompanyInfosQuery() : IQuery<GetDCCompanyInfosResult>;
public record GetDCCompanyInfosResult(IReadOnlyList<DCCompanyInfoDTO> DCCompanyInfo);
