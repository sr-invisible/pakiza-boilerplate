namespace Pakiza.Application.Features.DC.DCProject.Queries;

public record GetDCProjectsQuery() : IQuery<GetDCProjectsResult>;
public record GetDCProjectsResult(IReadOnlyList<DCProjectDTO> DCProject);
