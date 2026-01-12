using FluentValidation;
using FluentValidation.Results;
using IdentityService.API.Core.CQRS.QueryHandling;
using IdentityService.API.Domain.Entities;
using IdentityService.API.Domain.Repositories;
using IdentityService.API.DTO;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.API.Features;

public record GetTokenQuery(string Username, string Password) : Query<AuthenticationResponseDto>
{
    public override ValidationResult Validate()
    {
        return new GetTokenQueryValidator().Validate(this);
    }
}


public class GetTokenQueryHandler(IJwtRepository _jwt, UserManager<User> _userManager) : QueryHandler<GetTokenQuery, AuthenticationResponseDto>
{
    public override async Task<AuthenticationResponseDto> ExecuteQuery(GetTokenQuery query, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(query.Username);

        if (user == null)
            throw new UnauthorizedAccessException("Invalid username or password.");

        if (!await _userManager.CheckPasswordAsync(user, query.Password))
            throw new UnauthorizedAccessException("Invalid username or password.");

        var roles = await _userManager.GetRolesAsync(user);

        var authRequestDto = new AuthenticationRequestDto(
            Username: user.UserName,
            Id: user.Id,
            Name: user.Name,
            Roles: roles.ToList()
        );

        return _jwt.GenerateToken(authRequestDto);
    }
}


public class GetTokenQueryValidator : AbstractValidator<GetTokenQuery>
{
    public GetTokenQueryValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MaximumLength(100).WithMessage("Username must not exceed 100 characters.");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
            .MaximumLength(100).WithMessage("Password must not exceed 100 characters.");
    }
}