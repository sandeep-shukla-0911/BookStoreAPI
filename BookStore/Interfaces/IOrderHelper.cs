using BookStore.Response;

namespace BookStore.Interfaces
{
    public interface IOrderHelper
    {
        /// <summary>
        /// Prepares order details for a single order with order id
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        OrderDetailsDTO PrepareOrderDetails(int OrderId);
    }
}
