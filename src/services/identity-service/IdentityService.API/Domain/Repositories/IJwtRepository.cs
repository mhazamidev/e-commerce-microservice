using IdentityService.API.DTO;

namespace IdentityService.API.Domain.Repositories;

public interface IJwtRepository
{
    AuthenticationResponseDto GenerateToken(AuthenticationRequestDto options);
}
