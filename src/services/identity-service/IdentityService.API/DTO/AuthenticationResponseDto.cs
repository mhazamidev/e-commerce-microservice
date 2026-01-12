namespace IdentityService.API.DTO;

public record AuthenticationResponseDto(string Username, string Token, int ExpireIn);