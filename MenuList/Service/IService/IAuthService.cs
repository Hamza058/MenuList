using EntityLayer.Concrete;
using MenuList.Models;
using Org.BouncyCastle.Asn1.Ocsp;

namespace MenuList.Service.IService
{
    public interface IAuthService
    {
        Task<Response?> LoginAsync(User user);
    }
}
