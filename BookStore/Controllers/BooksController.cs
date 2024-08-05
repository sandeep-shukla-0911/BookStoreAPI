using BookStore.Constants;
using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using NSwag.Annotations;

namespace BookStore.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v1/[controller]/[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [OpenApiTag("Books Data")]
    public class BooksController : ControllerBase
    {
        private static readonly List<Books> allBooksData = MockData.books;
        private readonly ILogger<BooksController> _logger;

        public BooksController(ILogger<BooksController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetAllBooks")]
        [Authorize(Roles = "Admin,User")]
        [ResponseCache(CacheProfileName = ResponseCacheProfiles.CacheCommon)]
        public async Task<ActionResult<IEnumerable<Books>>> GetAllBooks()
        {
            try
            {
                await Task.Delay(Global.DelayInMilliSeconds);
                return Ok(allBooksData);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}",Name = "GetBookDetails")]
        [Authorize]
        [ResponseCache(CacheProfileName = ResponseCacheProfiles.CacheVaryById)]
        public async Task<ActionResult<IEnumerable<Books>>> GetBookDetails(int id)
        {
            try
            {
                await Task.Delay(Global.DelayInMilliSeconds);
                var bookDetails = allBooksData.FirstOrDefault(x => x.Id == id);
                if (bookDetails == null)
                {
                    object objBookNotFound = new { message = Messages.BookNotFound };
                    return NotFound(objBookNotFound);
                }
                else 
                {
                    return Ok(bookDetails); 
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
    }
}
