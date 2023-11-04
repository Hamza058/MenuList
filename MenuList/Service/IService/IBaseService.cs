using MenuList.Models;
using Org.BouncyCastle.Asn1.Ocsp;

namespace MenuList.Service.IService
{
    public interface IBaseService
    {
        Task<Response?> SendAsync(RequestDto request, bool withBearer = true);
    }
}
