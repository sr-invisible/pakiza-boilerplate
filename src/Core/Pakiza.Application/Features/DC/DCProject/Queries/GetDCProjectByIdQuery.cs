namespace Pakiza.Application.Features.DC.DCProject.Queries;

public record GetDCProjectByIdQuery(Guid Id) : IQuery<GetDCProjectByIdResult>;
public record GetDCProjectByIdResult(DCProjectDTO DCProject);
