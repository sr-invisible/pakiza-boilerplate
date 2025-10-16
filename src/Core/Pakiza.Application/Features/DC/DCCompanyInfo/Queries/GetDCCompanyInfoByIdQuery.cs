namespace Pakiza.Application.Features.DC.DCCompanyInfo.Queries;

public record GetDCCompanyInfoByIdQuery(Guid Id) : IQuery<GetDCCompanyInfoByIdResult>;
public record GetDCCompanyInfoByIdResult(DCCompanyInfoDTO DCCompanyInfo);
