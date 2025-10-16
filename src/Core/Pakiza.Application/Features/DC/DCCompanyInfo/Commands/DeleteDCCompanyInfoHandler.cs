namespace Pakiza.Application.Features.DC.DCCompanyInfo.Commands;

public class DeleteDCCompanyInfoHandler(IDCCompanyInfoService dCCompanyInfoService) : ICommandHandler<DeleteDCCompanyInfoCommand, DeleteDCCompanyInfoResult>
{
    public async Task<DeleteDCCompanyInfoResult> Handle(DeleteDCCompanyInfoCommand request, CancellationToken cancellationToken)
    {
        await dCCompanyInfoService.DeleteByIdAsync(request.Id);
        return new DeleteDCCompanyInfoResult(true);
    }
}