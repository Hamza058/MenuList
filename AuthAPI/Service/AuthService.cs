using AuthAPI.Models;
using AuthAPI.Service.IService;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http.HttpResults;
using Org.BouncyCastle.Crypto.Generators;
using System.Data;

namespace AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        UserManager um = new UserManager(new EFUserDal());
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(IJwtTokenGenerator jwtTokenGenerator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            var users = um.TGetList();
            var user = users.FirstOrDefault(x => x.UserName == loginRequest.UserName && BCrypt.Net.BCrypt.Verify(loginRequest.Password, x.Password));

            if (user == null)
            {
                return new LoginResponse() { User = null, Token = "" };
            }
            var token = _jwtTokenGenerator.GenerateToken(user);

            LoginResponse loginResponseDto = new LoginResponse()
            {
                User = user,
                Token = token
            };

            return loginResponseDto;
        }

        public Task<string> Register(LoginRequest register)
        {
            throw new NotImplementedException();
        }
    }
}
