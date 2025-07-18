using InnoSoft.InventorySystem.Core.Entities;
using InnoSoft.InventorySystem.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Application.Authentication
{
    public class AuthenticationService
    {

        private readonly TokenService _tokenService;
        public AuthenticationService(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        private List<User> Users = new List<User>
        {
            new User
            {
                Id = Guid.Parse("c63c0e6e-3b64-4d08-9f2b-4f3a41e9a7ef"),
                Username = "admin",
                Password = "admin123",
                UserRole = Roles.Admin,
            },
            new User
            {
                Id = Guid.Parse("9f972a0b-2d10-4e1d-8b6e-b798e4e5f7a9"),
                Username = "user",
                Password = "user123",
                UserRole = Roles.EndUser,
            }
        };


        public AuthenticationResult Authenticate(string username, string password)
        {
            var user = Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user == null)
            {
                throw new DomainException("InvalidCredentials");
            }
            
            var token = _tokenService.GenerateToken(user);
            return new AuthenticationResult() { UserName = user.Username, Token = token };
        }
    }
}
