namespace Pakiza.Application.Features.DC.DCCompanyInfo.Commands;

public class CreateDCCompanyInfoHandler(IDCCompanyInfoService dCCompanyInfoService) : ICommandHandler<CreateDCCompanyInfoCommand, CreateDCCompanyInfoResult>
{
    public async Task<CreateDCCompanyInfoResult> Handle(CreateDCCompanyInfoCommand request, CancellationToken cancellationToken)
    {
        request.DCCompanyInfo.Id = Guid.NewGuid();
        await dCCompanyInfoService.AddAsync(request.DCCompanyInfo);
        return new CreateDCCompanyInfoResult(request.DCCompanyInfo.Id);
    }
}