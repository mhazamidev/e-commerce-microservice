namespace IdentityService.API.DTO;

public record AuthenticationRequestDto(string Username, string Id, string Name, IList<string> Roles);
