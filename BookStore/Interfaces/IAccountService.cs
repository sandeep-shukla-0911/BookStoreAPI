using BookStore.Models;

namespace BookStore.Interfaces
{
    public interface IAccountService
    {
        /// <summary>
        /// Logs in a user by username and password and returns the user object on success otherwise null
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Users? Login(string userName, string password);
    }
}
