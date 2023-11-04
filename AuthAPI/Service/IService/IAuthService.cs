using AuthAPI.Models;

namespace AuthAPI.Service.IService
{
    public interface IAuthService
    {
        Task<string> Register(LoginRequest register);
        Task<LoginResponse> Login(LoginRequest loginRequest);
    }
}
