namespace Pakiza.Application.Features.DC.DCProject.Commands;

public class DeleteDCProjectHandler(IDCProjectService dCProjectService) : ICommandHandler<DeleteDCProjectCommand, DeleteDCProjectResult>
{
    public async Task<DeleteDCProjectResult> Handle(DeleteDCProjectCommand request, CancellationToken cancellationToken)
    {
        await dCProjectService.DeleteByIdAsync(request.Id);
        return new DeleteDCProjectResult(true);
    }
}