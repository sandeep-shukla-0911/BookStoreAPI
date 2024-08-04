using BookStore.Constants;
using BookStore.Interfaces;
using BookStore.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
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
    [OpenApiTag("Checkout Data")]
    public class CheckoutController : ControllerBase
    {
        private readonly ILogger<CheckoutController> _logger;
        private readonly ICheckoutHelper _checkoutHelper;

        public CheckoutController(ILogger<CheckoutController> logger, ICheckoutHelper checkoutHelper)
        {
            _logger = logger;
            _checkoutHelper = checkoutHelper;
        }

        [HttpPost(Name = "Checkout")]
        [Authorize]
        public async Task<ActionResult<object>> Checkout([FromBody] Checkout checkoutRequest)
        {
            try
            {
                await Task.Delay(Global.DelayInMilliSeconds);
                bool isCheckoutSuccess = _checkoutHelper.HandleCheckout(checkoutRequest);
                object result = new
                {
                    result = isCheckoutSuccess ? Messages.Success : Messages.Failure,
                    message = isCheckoutSuccess ? Messages.OrderPlacedSuccessfully : Messages.OrderFailedToPlace
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
    }
}
