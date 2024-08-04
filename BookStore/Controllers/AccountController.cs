using BookStore.Constants;
using BookStore.Interfaces;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NSwag.Annotations;

namespace BookStore.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v1/[controller]/[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [OpenApiTag("Account Data")]
    public class AccountController : ControllerBase
    {
        private IAccountService _userService;

        public AccountController(IAccountService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] Users user)
        {
            var validatedUser = _userService.Login(user.UserName, user.Password);

            if (validatedUser == null || validatedUser?.Token == String.Empty)
                return BadRequest(new { message = Messages.IncorrectUserNameOrPassword });

            object formattedResult = new
            {
                validatedUser.Id,
                validatedUser.UserName,
                validatedUser.Token,
                validatedUser.Role,
                validatedUser.FullName
            };

            var result = JsonConvert.SerializeObject(formattedResult,
            new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            });

            return Ok(result);
        }
    }
}
