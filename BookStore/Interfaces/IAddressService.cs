using BookStore.Models;

namespace BookStore.Interfaces
{
    public interface IAddressService
    {
        /// <summary>
        /// Gets all addresses in the database
        /// </summary>
        /// <returns></returns>
        List<Address> GetAllAddress();

        /// <summary>
        /// Gets a single address by address id
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns></returns>
        Address GetAddress(int addressId);
    }
}
