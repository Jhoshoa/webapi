using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LibraryService.WebAPI.Data;
using LibraryService.WebAPI.Services;

namespace LibraryService.WebAPI.Controllers
{
    [ApiController]
    [Route("api/libraries/{libraryId}/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ILibrariesService _librariesService;
        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService, ILibrariesService librariesService)
        {
            _librariesService = librariesService;
            _booksService = booksService;
        }

        // Implement the functionalities below
        [HttpGet]
        public async Task<IActionResult> GetAllBooks(int libraryId, [FromQuery] int[] ids)
        {
            var library = (await _librariesService.Get(new[] { libraryId })).FirstOrDefault();
            if (library == null)
                return NotFound();

            var books = await _booksService.Get(libraryId, ids);
            return Ok(books);
        }

        [HttpPost]
        public async Task<IActionResult> SaveBook(int libraryId, [FromBody] Book book)
        {
            var library = (await _librariesService.Get(new[] { libraryId })).FirstOrDefault();
            if (library == null)
                return NotFound($"Library with id {libraryId} not found.");

            var savedBook = await _booksService.Add(book);
            return Ok(book);
        }
    }
}