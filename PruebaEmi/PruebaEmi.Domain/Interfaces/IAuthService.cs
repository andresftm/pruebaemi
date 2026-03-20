using PruebaEmi.Domain.DTOs;
using PruebaEmi.Domain.Entities;

namespace PruebaEmi.Domain.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        string GenerateJwtToken(User user);
    }
}