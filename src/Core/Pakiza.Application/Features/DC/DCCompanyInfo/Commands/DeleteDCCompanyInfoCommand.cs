namespace Pakiza.Application.Features.DC.DCCompanyInfo.Commands;

public record DeleteDCCompanyInfoCommand(Guid Id) : ICommand<DeleteDCCompanyInfoResult>;
public record DeleteDCCompanyInfoResult(bool isDeleted);


public class DeleteDCCompanyInfoCommandValidator : AbstractValidator<DeleteDCCompanyInfoCommand>
{
    public DeleteDCCompanyInfoCommandValidator()
    {
        RuleFor(e => e.Id).NotNull().NotEmpty();
    }
}
