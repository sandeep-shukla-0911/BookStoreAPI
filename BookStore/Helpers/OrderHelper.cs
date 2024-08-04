using BookStore.Constants;
using BookStore.Interfaces;
using BookStore.Models;
using BookStore.Response;

namespace BookStore.Helpers
{
    public sealed class OrderHelper: IOrderHelper
    {
        private readonly IUserHelper _userHelper;
        private readonly IOrderService _orderService;
        private readonly IBookService _bookService;
        private readonly IAddressService _addressService;

        public OrderHelper(IUserHelper userHelper, IOrderService orderService, IBookService bookService, IAddressService addressService)
        {
            _userHelper = userHelper ?? throw new ArgumentNullException(nameof(userHelper));
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
            _addressService = addressService ?? throw new ArgumentNullException(nameof(addressService));
        }
        public OrderDetailsDTO PrepareOrderDetails(int OrderId)
        {
            // get order details
            var order = _orderService.GetOrder(OrderId);
            if (order == null)
            {
                throw new Exception(Messages.OrderNotFound);
            }
            // get order line items
            var orderLineItems = _orderService.GetAllOrderLineItems(OrderId);
            if (orderLineItems == null)
            {
                throw new Exception(Messages.OrderLineItemsNotFound);
            }
            // get user details
            var user = _userHelper.GetUser(order.UserId);
            if (user == null)
            {
                throw new Exception(Messages.UserNotFound);
            }
            var Books = new List<Books>();
            // get book details
            foreach (var item in orderLineItems)
            {
                var book = _bookService.GetBook(item.BookId);
                if (book == null)
                {
                    throw new Exception(Messages.BookNotFound);
                }
                else
                {
                    Books.Add(book);
                }
            }

            // get address details
            var address = _addressService.GetAddress(order.AddressId);
            if (address == null)
            {
                throw new Exception(Messages.AddressNotFound);
            }

            // prepare order details
            return new OrderDetailsDTO {
                OrderId = order.Id,
                UserId = order.UserId,
                TotalAmount = order.TotalAmount,
                OrderDate = order.OrderDate,
                DeliveryDate = order.DeliveryDate,
                UserName = user.UserName,
                OrderItems = Books,
                deliveryAddress = address
            };
        }
    }
}
