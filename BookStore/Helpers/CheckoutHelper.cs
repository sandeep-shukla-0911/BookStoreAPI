using BookStore.Data;
using BookStore.Interfaces;
using BookStore.Models;
using BookStore.Request;
using System.Data;

namespace BookStore.Helpers
{
    public sealed class CheckoutHelper : ICheckoutHelper
    {
        private readonly IAccountService _userService;
        private readonly IOrderService _orderService;
        private readonly IBookService _bookService;
        private readonly IAddressService _addressService;
        private readonly IUserHelper _userHelper;

        public CheckoutHelper(IAccountService userService, IOrderService orderService, IBookService bookService, IAddressService addressService, IUserHelper userHelper)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
            _addressService = addressService ?? throw new ArgumentNullException(nameof(addressService));
            _userHelper = userHelper ?? throw new ArgumentNullException(nameof(userHelper));
        }
        public bool HandleCheckout(Checkout checkoutRequest)
        {
            var LstBooks = new List<Books>();
            // get book details
            foreach (var item in checkoutRequest?.BookIds)
            {
                var book = _bookService.GetBook(item);
                if (book == null)
                {
                    throw new Exception("Book not found");
                }
                else
                {
                    LstBooks.Add(book);
                }
            }

            // get user details
            var user = _userHelper.GetCurrentUser();
            if (user == null)
            {
                throw new Exception("User not found");
            }

            // get address details
            var address = _addressService.GetAllAddress()?.SingleOrDefault(x => x.UserId == user.Id);
            if (address == null)
            {
                throw new Exception("Address not found");
            }

            // calculate total amount
            var totalAmount = LstBooks.Sum(x => x.Price);

            // get latest Order Id from mockData
            var latestOrderId = MockData.orders.Select(x => x.Id).Max();

            // add entry into orders
            Orders newOrder = new()
            {
                Id = latestOrderId + 1,
                OrderDate = DateTime.UtcNow,
                UserId = user.Id ?? user.Id.Value,
                TotalAmount = totalAmount,
                AddressId = address.Id
            };
            MockData.orders.Add(newOrder);

            // add entry into orderLines
            foreach (var items in LstBooks)
            {
                // get latest Order Id from mockData
                var latestOrderLineId = MockData.orderLines.Select(x => x.Id).Max();
                OrderLine line = new()
                {
                    Id = latestOrderLineId + 1,
                    OrderId = newOrder.Id,
                    BookId = items.Id,
                    Quantity = 1,
                    Price = items.Price ?? items.Price.Value
                };
                MockData.orderLines.Add(line);
            }

            return true;
        }
    }
}
