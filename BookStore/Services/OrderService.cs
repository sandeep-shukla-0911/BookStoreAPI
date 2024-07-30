using BookStore.Data;
using BookStore.Interfaces;
using BookStore.Models;

namespace BookStore.Services
{
    public class OrderService: IOrderService
    {
        public readonly IConfiguration _configuration;
        public OrderService(IConfiguration configuration) => _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        public List<Orders> GetAllOrders(int userId)
        {
            return MockData.orders.Where(x => x.UserId == userId).ToList();
        }

        public Orders GetOrder(int orderId)
        {
           return MockData.orders.FirstOrDefault(x => x.Id == orderId);
        }

        public List<OrderLine> GetAllOrderLineItems(int orderId)
        {
            return MockData.orderLines.Where(x => x.OrderId == orderId).ToList();
        }
    }
}
