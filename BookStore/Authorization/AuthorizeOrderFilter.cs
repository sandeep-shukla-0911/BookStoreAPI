using BookStore.Constants;
using BookStore.Data;
using BookStore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookStore.Authorization
{
    public class AuthorizeOrderFilter : IAsyncAuthorizationFilter
    {
        private readonly IUserHelper _userHelper;

        public AuthorizeOrderFilter(IUserHelper userHelper)
        {
            _userHelper = userHelper ?? throw new ArgumentNullException(nameof(userHelper));
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context is not null)
            {
                var orderId = context.RouteData?.Values[Global.Id].ToString();
                var action = context.HttpContext.Request.Method;
                var user = _userHelper.GetCurrentUser();
                if (user == null)
                {
                    context.Result = new ForbidResult();
                    return;
                }

                var order = MockData.orders.SingleOrDefault(x => x.Id == Convert.ToInt32(orderId));
                if (order == null)
                {
                    context.Result = new NotFoundResult();
                    return;
                }

                if(!(action == "GET" && (user.Id == order.UserId || user.Role.Equals(Global.UserRoleAdmin))))
                {
                    context.Result = new ForbidResult();
                    return;
                }
            }
        }
    }
}
