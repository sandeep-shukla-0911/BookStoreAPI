using BookStore.Data;
using BookStore.Interfaces;
using BookStore.Models;
using System.Security.Claims;

namespace BookStore.Helpers
{
    public sealed class UserHelper : IUserHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public readonly string ClaimIdentifierUserId = "UserId";

        public UserHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public Users? GetCurrentUser()
        {
            var identity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new Users
                {
                    Id = Convert.ToInt32(userClaims.FirstOrDefault(x => x.Type == ClaimIdentifierUserId)?.Value),
                    UserName = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value,
                    Role = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value
                };
            }
            return null;
        }


        public Users? GetUser(int UserId)
        {
            return MockData.users.SingleOrDefault(x => x.Id == UserId);
        }
    }
}
