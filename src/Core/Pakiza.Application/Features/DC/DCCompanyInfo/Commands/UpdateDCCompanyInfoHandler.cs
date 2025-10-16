namespace Pakiza.Application.Features.DC.DCCompanyInfo.Commands;

public class UpdateDCCompanyInfoHandler(IDCCompanyInfoService dCCompanyInfoService) : ICommandHandler<UpdateDCCompanyInfoCommand, UpdateDCCompanyInfoResult>
{
    public async Task<UpdateDCCompanyInfoResult> Handle(UpdateDCCompanyInfoCommand request, CancellationToken cancellationToken)
    {
        await dCCompanyInfoService.UpdateAsync(request.DCCompanyInfoDTO);
        return new UpdateDCCompanyInfoResult(request.DCCompanyInfoDTO.Id);
    }
}