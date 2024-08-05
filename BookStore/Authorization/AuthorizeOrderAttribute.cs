using Microsoft.AspNetCore.Mvc;

namespace BookStore.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class AuthorizeOrderAttribute : TypeFilterAttribute
    {
        public AuthorizeOrderAttribute() : base(typeof(AuthorizeOrderFilter))
        {
        }
    }
}

