using MediatR;

namespace HebrewVerb.Application.Feature.Authentication.Commands.AuthUser;

public record AuthUserCommand : IRequest<AuthResponseDto>
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
}
