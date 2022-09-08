using LibraryBackEnd.Model;
using LibraryBackEnd.Model.BookDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using System.Text.Json;

namespace LibraryBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("OpenCORSPolicy")]
    public class BooksController : ControllerBase
    {
        private readonly BookDbContext _db;

        public BooksController(BookDbContext db)
        {
            _db = db;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDetailDTO>> GetBook(int id)
        {
            
            var record =  await _db.Books.FirstOrDefaultAsync(r => r.ID == id);
            int averageRating = 0;
            if((await _db.Ratings.FirstOrDefaultAsync(rt => rt.BookID == id)) != null)
            {
                averageRating = (int)(from rt in _db.Ratings where rt.BookID == id select rt.Score).Average();
            }
            BookDetailDTO result = new BookDetailDTO()
            {
                ID = record.ID,
                Title = record.Title,
                Author = record.Author,
                CoverUrl = record.CoverUrl,
                Content = record.Content,
                AverageRating = averageRating,
                Reviews = (from rw in _db.Reviews where rw.BookID == id select new ReviewDTO()
                {
                    ID = rw.ID,
                    Message = rw.Message,
                    Reviewer = rw.Rewiewer
                }).ToList(),
            };
            return result;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookDTO>>> GetAllBooks()
        {
            string orderingParameter = Request.Query.FirstOrDefault(p => p.Key == "order").Value;
            if(orderingParameter == "author")
            {
                var books = from b in _db.Books
                            select new BookDTO()
                            {
                                ID = b.ID,
                                Title = b.Title,
                                Author = b.Author,
                                Raiting = (_db.Ratings.FirstOrDefault(rt => rt.BookID == b.ID)) == null?0:
                                (int)(from rt in _db.Ratings where rt.BookID == b.ID select rt.Score).Average(),
                                ReviewsNumber = (from rw in _db.Reviews where rw.BookID == b.ID select rw).Count()
                            };

                return await books.OrderBy(p => p.Author).ToListAsync();
            }
            else if(orderingParameter == "title")
            {
                var books = from b in _db.Books
                            select new BookDTO()
                            {
                                ID = b.ID,
                                Title = b.Title,
                                Author = b.Author,
                                Raiting = (_db.Ratings.FirstOrDefault(rt => rt.BookID == b.ID)) == null ? 0 :
                                (int)(from rt in _db.Ratings where rt.BookID == b.ID select rt.Score).Average(),
                                ReviewsNumber = (from rw in _db.Reviews where rw.BookID == b.ID select rw).Count()
                            };

                return await books.OrderBy(p => p.Title).ToListAsync();
            }
            else
            {
                return Content("Something wrong");
            }
        }

        [HttpPost("save")]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            var record = await _db.Books.FirstOrDefaultAsync(r => r.ID == book.ID);
            if (record == null)
            {
                _db.Books.Add(book);
            }
            else
            {
                record.Title = book.Title;
                record.Author = book.Author;
                record.CoverUrl = book.CoverUrl;
                record.Content = book.Content;
                record.Genre = book.Genre;
            }
            await _db.SaveChangesAsync();

            return book;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var record = await _db.Books.FindAsync(id);
            if(record == null)
            {
                return NotFound();
            }
            _db.Books.Remove(record);
            await _db.SaveChangesAsync();

            return NoContent();
        }
        [HttpPut("{id}/review")]
        public async Task<ActionResult<Review>> AddReview(Review review)
        {
            var record = await _db.Reviews.FirstOrDefaultAsync(r => r.ID == review.ID);
            if (record == null)
            {
                _db.Reviews.Add(review);
            }
            else
            {
                return Content("Review with this Id already exist!");
            }
            await _db.SaveChangesAsync();
            return Ok(review.BookID);
        }
        [HttpPut("{id}/rate")]
        public async Task<ActionResult> AddRating(Rating rating)
        {
            var record = await _db.Ratings.FirstOrDefaultAsync(r => r.ID == rating.ID);
            if (record == null)
            {
                _db.Ratings.Add(rating);
            }
            else
            {
                return Content("Raiting with this Id already exist!");
            }
            await _db.SaveChangesAsync();
            return Ok(rating.Score);
        }
    }
}
