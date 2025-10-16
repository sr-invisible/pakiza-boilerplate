namespace Pakiza.Application.Features.DC.DCCompanyInfo.Commands;

public record UpdateDCCompanyInfoCommand(DCCompanyInfoDTO DCCompanyInfoDTO) : ICommand<UpdateDCCompanyInfoResult>;
public record UpdateDCCompanyInfoResult(Guid Id);

public class UpdateDCCompanyInfoCommandValidator : AbstractValidator<UpdateDCCompanyInfoCommand>
{
    public UpdateDCCompanyInfoCommandValidator()
    {
        RuleFor(e => e.DCCompanyInfoDTO.Id).NotNull().NotEmpty();
        RuleFor(e => e.DCCompanyInfoDTO.CompanyName).NotEmpty().MaximumLength(200);
        RuleFor(e => e.DCCompanyInfoDTO.Address).MaximumLength(300);
        RuleFor(e => e.DCCompanyInfoDTO.ContactPerson).MaximumLength(200);
        RuleFor(e => e.DCCompanyInfoDTO.Phone).NotEmpty().MaximumLength(50);
        RuleFor(e => e.DCCompanyInfoDTO.Email).MaximumLength(50);
        RuleFor(e => e.DCCompanyInfoDTO.WebUrl).MaximumLength(50);
    }
}
