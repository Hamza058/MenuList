using EntityLayer.Concrete;
using MenuList.Models;
using MenuList.Service.IService;
using MenuList.Utility;

namespace MenuList.Service
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;
        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<Response?> LoginAsync(User user)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = user,
                Url = SD.AuthAPIBase + "/api/auth"
            }, withBearer: false);
        }
    }
}
