using BookStore.Models;

namespace BookStore.Interfaces
{
    public interface IUserHelper
    {
        /// <summary>
        /// Gets the current user.
        /// </summary>
        /// <returns></returns>
        Users? GetCurrentUser();

        /// <summary>
        /// Gets the user with the given UserId.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        Users? GetUser(int UserId);
    }
}
