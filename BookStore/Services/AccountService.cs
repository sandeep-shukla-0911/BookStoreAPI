using BookStore.Constants;
using BookStore.Data;
using BookStore.Interfaces;
using BookStore.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStore.Services
{
    public class AccountService : IAccountService
    {
        public readonly IConfiguration _configuration;
        public AccountService(IConfiguration configuration) => _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        public Users? Login(string userName, string password)
        {
            var user = MockData.users.SingleOrDefault(x => x.UserName == userName && x.Password == password);

            // return null if user not found
            if (user == null)
            {
                return null;
            }

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>(Global.JWTSecret));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(Global.ClaimIdentifierUserId, user.Id.ToString())
                ]),

                Expires = DateTime.UtcNow.AddMinutes(Global.JWTExpiryInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user;
        }
    }
}
