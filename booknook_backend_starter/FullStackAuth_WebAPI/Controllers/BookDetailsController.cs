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
                // Retrieve the authenticated user's ID from the JWT token
                string userId = User.FindFirstValue("id");

                // Get reviews related to the bookId
                var reviews = _context.Reviews
                    .Where(r => r.BookId = bookId)
                    .Include(r => r.User) // Include User for usernames
                    .Select(r => new ReviewWithUserDTO
                    {
                        Id = r.Id,
                        Text = r.Text,
                        Rating = r.Rating,
                        UserName = r.User.UserName // Include only necessary user properties
                    })
                    .ToList();

                // Calculate the average rating
                double averageRating = reviews.Any() ? reviews.Average(r => r.Rating) : 0;

                // Check if the logged-in user has favorited this book
                bool isFavorited = _context.Favorites.Any(f => f.UserId == userId && f.BookId == bookId);

                // Create the BookDetailsDto
                var bookDetails = new BookDetailsDTO
                {
                    Reviews = reviews,
                    AverageRating = averageRating,
                    IsFavorited = isFavorited
                };

                // Return the BookDetailsDto as a 200 OK response
                return Ok(bookDetails);
            }
            catch (Exception ex)
            {
                // If an error occurs, return a 500 internal server error with the error message
                return StatusCode(500, ex.Message);
            }
        }
    }
}
    
