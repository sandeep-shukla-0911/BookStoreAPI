using BookStore.Constants;
using BookStore.Data;
using BookStore.Interfaces;
using BookStore.Response;
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
    [OpenApiTag("Orders Data")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IOrderHelper _orderHelper;
        private readonly IUserHelper _userHelper;

        public OrdersController(ILogger<OrdersController> logger, IOrderHelper orderHelper, IUserHelper userHelper)
        {
            _logger = logger;
            _orderHelper = orderHelper;
            _userHelper = userHelper;
        }

        [HttpGet(Name = "GetAllOrders")]
        [Authorize(Roles = "admin,user")]
        public async Task<ActionResult<IEnumerable<OrderDetailsDTO>>> GetAllOrders()
        {
            try
            {
                await Task.Delay(Global.DelayInMilliSeconds);
                var currentUser = _userHelper.GetCurrentUser();

                List<OrderDetailsDTO> orderDetailsDTO = new();

                if (currentUser.Role.Equals("admin"))
                {
                    foreach (var order in MockData.orders)
                    {
                        orderDetailsDTO.Add(_orderHelper.PrepareOrderDetails(order.Id));
                    }
                } else
                {
                    var result = MockData.orders.FindAll(x => x.UserId == currentUser.Id);
                    foreach (var order in result)
                    {
                        orderDetailsDTO.Add(_orderHelper.PrepareOrderDetails(order.Id));
                    }
                }

                return Ok(orderDetailsDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}", Name = "GetOrderDetails")]
        [Authorize]
        [ResponseCache(CacheProfileName = ResponseCacheProfiles.CacheVaryById)]
        public async Task<ActionResult<OrderDetailsDTO>> GetOrderDetails(int id)
        {
            try
            {
                await Task.Delay(Global.DelayInMilliSeconds);
                var orderDetails = MockData.orders.FirstOrDefault(x => x.Id == id);
                if (orderDetails == null)
                {
                    object objOrderNotFound = new { message = Messages.OrderNotFound };
                    return NotFound(objOrderNotFound);
                }
                else 
                {
                    return Ok(_orderHelper.PrepareOrderDetails(id)); 
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
    }
}
