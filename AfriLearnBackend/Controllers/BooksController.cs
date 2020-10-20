using AfriLearn.Models;
using AfriLearnBackend.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AfriLearnBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksRepository _booksRepository;
        public BooksController(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        [HttpPost("GetBook")]
        public async Task<ActionResult> GetBook(Book book)
        {
            return Ok(await _booksRepository.GetBlobAsync(book));
        }
          
        [HttpGet("getallbooknames")]
        public async Task<ActionResult> GetAllBookNames()
        {
            return Ok(await _booksRepository.GetAllBookNames());
        }
    }
}
