using Pakiza.Application.DTOs.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pakiza.Application.Features.Authentication.Commands
{
    public record TokenRequest : TokenDTO;

    public record RefreshCommand(TokenRequest TokenRequest) : ICommand<RefreshResult>;
    public record RefreshResult(TokenDTO TokenResponse);

    public class RefreshTokenValidator : AbstractValidator<TokenRequest>
    {
        public RefreshTokenValidator()
        {
            RuleFor(x => x.AccessToken)
                .NotEmpty().WithMessage("Access Token is required.");
            RuleFor(x => x.RefreshToken)
               .NotEmpty().WithMessage("Refresh Token is required.");
        }
    }
}
