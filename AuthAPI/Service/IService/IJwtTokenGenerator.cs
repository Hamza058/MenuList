using EntityLayer.Concrete;

namespace AuthAPI.Service.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
