using BookStore.Data;
using BookStore.Interfaces;
using BookStore.Models;

namespace BookStore.Services
{
    public class BookService: IBookService
    {
        public readonly IConfiguration _configuration;
        public BookService(IConfiguration configuration) => _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        public List<Books> GetAllBooks()
        {
            return MockData.books;
        }

        public Books GetBook(int bookId)
        {
           return MockData.books.FirstOrDefault(x => x.Id == bookId);
        }
    }
}
