namespace Pakiza.Persistence.Services.DC;

public class DCProjectService : IDCProjectService
{
    private readonly IDCProjectRepository _iDCProjectRepository;

    public DCProjectService(IDCProjectRepository iDCProjectRepository)
    {
        _iDCProjectRepository = iDCProjectRepository;
    }

    public async Task<int> AddAsync(DCProjectDTO entity)
    {
        return await _iDCProjectRepository.AddAsync(entity.Adapt<DCProject>());
    }

    public async Task<int> DeleteByIdAsync(Guid id)
    {
        return await _iDCProjectRepository.RemoveAsync(id);
    }

    public async Task<IReadOnlyList<DCProjectDTO>> GetAllAsync()
    {
        var projects = (await _iDCProjectRepository.GetAllAsync(false)).Adapt<IReadOnlyList<DCProjectDTO>>();
        return projects;
    }

    public async Task<DCProjectDTO> GetByIdAsync(Guid id)
    {
        var project = (await _iDCProjectRepository.GetByIdAsync(id)).Adapt<DCProjectDTO>();
        return project;
    }

    public async Task<int> UpdateAsync(DCProjectDTO entity)
    {
        if (await _iDCProjectRepository.GetByIdAsync(entity.Id) is DCProject dbEntity && dbEntity is not null)
        {
            dbEntity.ProjectName = entity.ProjectName;
            dbEntity.Status = Status.Active;

            return await _iDCProjectRepository.UpdateAsync(dbEntity);
        }
        throw new ArgumentNullException(nameof(entity));
    }
}