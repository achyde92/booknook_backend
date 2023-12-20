using System;
using System.Security.Claims;
using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.DataTransferObjects;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FullStackAuth_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookDetailsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BookDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }
         // GET: api/bookdetails/{bookId}
        [HttpGet("{bookId}"), Authorize]
        public IActionResult GetBookDetails(int bookId)
        {
            try
            {
                string userId = User.FindFirstValue("id");

                var reviews = _context.Reviews
                    .Where(r => r.BookId == bookId)
                    .Include(r => r.User)
                    .Select(r => new ReviewWithUserDTO
                    {
                        Users = r.User.Select(u => new UserForDisplayDto
                        {
                            Id = u.Id,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            UserName = u.UserName,
                        }),
                        Text = r.Text,
                        Rating = r.Rating,
                    })
                    .ToList();

                double averageRating = reviews.Any() ? reviews.Average(r => r.Rating) : 0;

                bool isFavorited = _context.Favorites.Any(f => f.UserId == userId && f.BookId == bookId);

                var bookDetails = new BookDetailsDTO
                {
                    Reviews = reviews,
                    AverageRating = averageRating,
                    IsFavorited = isFavorited,
                };
                  
                return Ok(bookDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
    
