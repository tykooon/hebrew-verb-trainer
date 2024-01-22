using Ardalis.Result;
using HebrewVerb.Application.Interfaces.Identity;
using MediatR;

namespace HebrewVerb.Application.Feature.Authentication.Commands.CreateUser;

public class CreateUserCommanrHandler : IRequestHandler<CreateUserCommand, Result<int>>
{
    private readonly IIdentityService _identityService;

    public CreateUserCommanrHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _identityService.CreateUserAsync(
            request.UserName!,
            request.Email!,
            request.Password!);

        if (!result.IsSuccess)
        {
            var errors = string.Join(Environment.NewLine, result.Errors);
            throw new Exception($"Unable to create {request.UserName}.{Environment.NewLine}{errors}");
        }
        var addUserToRole = await _identityService.AddToRolesAsync(result.Value, request.Roles!);
        if (!addUserToRole.IsSuccess)
        {
            var errors = string.Join(Environment.NewLine, addUserToRole.Errors);
            throw new Exception($"Unable to add {request.UserName} to assigned role/s.{Environment.NewLine}{errors}");
        }
        return result.Value;
    }
}
