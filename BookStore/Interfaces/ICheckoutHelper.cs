using BookStore.Request;

namespace BookStore.Interfaces
{
    public interface ICheckoutHelper
    {
        /// <summary>
        /// Handles the checkout process and adds the order to the database. Returns true if successful, false otherwise.
        /// </summary>
        /// <param name="checkoutRequest"></param>
        /// <returns></returns>
        bool HandleCheckout(Checkout checkoutRequest);
    }
}
