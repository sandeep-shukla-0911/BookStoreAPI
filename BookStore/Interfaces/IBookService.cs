using BookStore.Models;

namespace BookStore.Interfaces
{
    public interface IBookService
    {
        /// <summary>
        /// Gets all books in the database
        /// </summary>
        /// <returns></returns>
        List<Books> GetAllBooks();

        /// <summary>
        /// Get a single book by book id
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        Books GetBook(int bookId);
    }
}
