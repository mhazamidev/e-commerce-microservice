using Microsoft.AspNetCore.Identity;

namespace IdentityService.API.Domain.Entities;

public class User : IdentityUser
{
    public string Name { get; set; }
}
