namespace Pakiza.Persistence.Services.DC;

public class DCCompanyInfoService : IDCCompanyInfoService
{
    private readonly IDCCompanyInfoRepository _iDCCompanyInfoRepository;

    public DCCompanyInfoService(IDCCompanyInfoRepository iDCCompanyInfoRepository)  
    {
        _iDCCompanyInfoRepository = iDCCompanyInfoRepository;
    }
    public async Task<int> AddAsync(DCCompanyInfoDTO entity)
    {
        try
        {
            return await _iDCCompanyInfoRepository.AddAsync(entity.Adapt<DCCompanyInfo>());
        }
        catch (Exception ex)
        {

            throw;
        }
        
    }

    public async Task<int> DeleteByIdAsync(Guid id)
    {
        return await _iDCCompanyInfoRepository.RemoveAsync(id);
    }

    public async Task<IReadOnlyList<DCCompanyInfoDTO>> GetAllAsync()
    {
        return (await _iDCCompanyInfoRepository.GetAllAsync(false)).Adapt<IReadOnlyList<DCCompanyInfoDTO>>();
    }

    public async Task<DCCompanyInfoDTO> GetByIdAsync(Guid id)
    {
        return (await _iDCCompanyInfoRepository.GetByIdAsync(id)).Adapt<DCCompanyInfoDTO>();
    }

    public async Task<int> UpdateAsync(DCCompanyInfoDTO entity)
    {
        if(await _iDCCompanyInfoRepository.GetByIdAsync(entity.Id) is DCCompanyInfo dbEntity && dbEntity is not null)
        {
            dbEntity.CompanyName = entity.CompanyName;
            dbEntity.Address = entity.Address ?? string.Empty;
            dbEntity.ContactPerson = entity.ContactPerson ?? string.Empty;
            dbEntity.Phone = entity.Phone;
            dbEntity.Email = entity.Email ?? string.Empty;
            dbEntity.WebUrl = entity.WebUrl ?? string.Empty;
            dbEntity.Status = Status.Active;

            return await _iDCCompanyInfoRepository.UpdateAsync(dbEntity);
        }
        throw new ArgumentNullException(nameof(entity));
    }
}
