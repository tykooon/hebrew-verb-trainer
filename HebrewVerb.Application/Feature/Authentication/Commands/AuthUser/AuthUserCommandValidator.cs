using FluentValidation;

namespace HebrewVerb.Application.Feature.Authentication.Commands.AuthUser;

public class AuthUserCommandValidator : AbstractValidator<AuthUserCommand>
{
    public AuthUserCommandValidator()
    {
        RuleFor(command => command.UserName)
            .MinimumLength(4)
            .MaximumLength(16);
    }
}
