using HebrewVerb.Application.Interfaces.Identity;
using MediatR;

namespace HebrewVerb.Application.Feature.Authentication.Commands.AuthUser;

public class AuthUserCommandHandler : IRequestHandler<AuthUserCommand, AuthResponseDto>
{
    private readonly IIdentityService _identityService;
    private readonly IAppUserService _appUserService;
    private readonly IJwtUtils _jwtUtils;


    public AuthUserCommandHandler(IIdentityService identityService, IJwtUtils jwtUtils, IAppUserService appUserService)
    {
        _identityService = identityService;
        _jwtUtils = jwtUtils;
        _appUserService = appUserService;
    }

    public async Task<AuthResponseDto> Handle(AuthUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _identityService.AuthenticateAsync(request.UserName!, request.Password!);
        if (!result)
        {
            throw new Exception("Invalid Username or Password");
        }
        var details = await _appUserService.GetUserDetailsByUserNameAsync(request.UserName!);
        string token = _jwtUtils.GenerateToken(details.UserId, details.Username, details.Roles);
        return new AuthResponseDto
        {
            UserId = details.UserId,
            Email = details.Email,
            Name = details.Username,
            Roles = details.Roles,
            Token = token,
        };


    }
}
