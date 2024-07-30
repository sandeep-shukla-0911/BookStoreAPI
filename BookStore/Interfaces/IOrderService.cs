using BookStore.Models;

namespace BookStore.Interfaces
{
    public interface IOrderService
    {
        /// <summary>
        /// Gets all orders for a user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<Orders> GetAllOrders(int userId);

        /// <summary>
        /// Gets a single order by order id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Orders GetOrder(int orderId);

        /// <summary>
        /// Gets all order line items for a single order
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        List<OrderLine> GetAllOrderLineItems(int orderId);
    }
}
