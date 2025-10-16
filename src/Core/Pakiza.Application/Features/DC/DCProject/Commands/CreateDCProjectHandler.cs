namespace Pakiza.Application.Features.DC.DCProject.Commands;

public class CreateDCProjectHandler(IDCProjectService dCProjectService) : ICommandHandler<CreateDCProjectCommand, CreateDCProjectResult>
{
    public async Task<CreateDCProjectResult> Handle(CreateDCProjectCommand request, CancellationToken cancellationToken)
    {
        request.DCProject.Id = Guid.NewGuid();
        await dCProjectService.AddAsync(request.DCProject);
        return new CreateDCProjectResult(request.DCProject.Id);
    }
}