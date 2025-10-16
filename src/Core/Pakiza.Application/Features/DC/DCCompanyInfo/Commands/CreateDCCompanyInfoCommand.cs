namespace Pakiza.Application.Features.DC.DCCompanyInfo.Commands;

public record CreateDCCompanyInfoCommand(DCCompanyInfoDTO DCCompanyInfo) : ICommand<CreateDCCompanyInfoResult>;
public record CreateDCCompanyInfoResult(Guid id);

public class CreateDCCompanyInfoCommandValidator : AbstractValidator<CreateDCCompanyInfoCommand>
{
    public CreateDCCompanyInfoCommandValidator()
    {
        RuleFor(e => e.DCCompanyInfo.CompanyName).MaximumLength(200);
        RuleFor(e => e.DCCompanyInfo.Address).MaximumLength(300);
        RuleFor(e => e.DCCompanyInfo.ContactPerson).MaximumLength(200);
        RuleFor(e => e.DCCompanyInfo.Phone).NotEmpty().MaximumLength(50);
        RuleFor(e => e.DCCompanyInfo.Email).MaximumLength(50);
        RuleFor(e => e.DCCompanyInfo.WebUrl).MaximumLength(50);
    }
}
