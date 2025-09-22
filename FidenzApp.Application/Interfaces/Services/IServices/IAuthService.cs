using FidenzApp.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FidenzApp.Application.Interfaces.Services.IServices
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterDto registerDto);
        Task<String> LoginAsync(LoginDto loginDto);
    }
}
