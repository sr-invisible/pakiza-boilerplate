namespace Pakiza.Application.Features.DC.DCProject.Commands;

public class UpdateDCProjectHandler(IDCProjectService dCProjectService) : ICommandHandler<UpdateDCProjectCommand, UpdateDCProjectResult>
{
    public async Task<UpdateDCProjectResult> Handle(UpdateDCProjectCommand request, CancellationToken cancellationToken)
    {
        await dCProjectService.UpdateAsync(request.DCProjectDTO);
        return new UpdateDCProjectResult(request.DCProjectDTO.Id);
    }
}