using Ardalis.Result;
using MediatR;

namespace HebrewVerb.Application.Feature.Authentication.Commands.CreateUser;

public record CreateUserCommand : IRequest<Result<int>>
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    public List<string> Roles { get; set; } = [ "Anonymous" ];
}
