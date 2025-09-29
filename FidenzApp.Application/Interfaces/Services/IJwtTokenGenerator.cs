using FidenzApp.Domain.Entities;

namespace FidenzApp.Application.Interfaces.Services
{
    public interface IJwtTokenGenerator
    {
        Task<string> GenerateToken(ApplicationUser user);
    }
}
