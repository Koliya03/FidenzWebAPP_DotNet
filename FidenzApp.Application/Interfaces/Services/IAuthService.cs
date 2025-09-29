using FidenzApp.Application.DTO;

namespace FidenzApp.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterDto registerDto);
        Task<string> LoginAsync(LoginDto loginDto);
    }
}
