using EntityLayer.Concrete;

namespace MenuList.Models
{
    public class LoginResponse
    {
        public User User { get; set; }
        public string Token { get; set; }
    }
}
