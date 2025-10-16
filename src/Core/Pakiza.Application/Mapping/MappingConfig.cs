using Mapster;
using Pakiza.Application.DTOs.User;
using Pakiza.Application.Request;
using Pakiza.Domain.Entities.DC;
namespace Pakiza.Application.Mapping;
public static class MappingConfig
{
    public static void Configure()
    {
        try
        {
            TypeAdapterConfig<User, UserDTO>.NewConfig();
            TypeAdapterConfig<Role, RoleDTO>.NewConfig();
            TypeAdapterConfig<User,RegisterDTO>.NewConfig();
            TypeAdapterConfig<User,UserInfo>.NewConfig();
            TypeAdapterConfig<Product,ProductDTO>.NewConfig();
            TypeAdapterConfig<ProductDTO,ProductRequest>.NewConfig();

            TypeAdapterConfig<SqlScriptDTO, SqlScriptRequest>.NewConfig();
            TypeAdapterConfig<SqlScript,SqlScriptDTO>.NewConfig();



            TypeAdapterConfig<DCCompanyInfoDTO, DCCompanyInfo>.NewConfig()
                .Ignore(des => des.DCCompanyProjects)
                .Map(des => des.IsActive, src => src.IsActive == null ? true : src.IsActive);
            TypeAdapterConfig<DCCompanyInfo, DCCompanyInfoDTO>.NewConfig();
            TypeAdapterConfig<DCCompanyInfoUpsertRequest, DCCompanyInfoDTO>.NewConfig()
                .Map(des => des.IsActive, src => src.IsActive == null ? true : src.IsActive);

            TypeAdapterConfig<DCProjectDTO, DCProject>.NewConfig()
                .Map(des => des.IsActive, src => src.IsActive == null ? true : src.IsActive);
            TypeAdapterConfig<DCProject, DCProjectDTO>.NewConfig();
            TypeAdapterConfig<DCProjectUpsertRequest, DCProjectDTO>.NewConfig()
                .Map(des => des.IsActive, src => src.IsActive == null ? true : src.IsActive);
        }
        catch (TypeInitializationException ex)
        {
            Console.WriteLine(ex.InnerException!.ToString());
        }


    }
}
