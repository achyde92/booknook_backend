using System;
using System.Security.Claims;
using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FullStackAuth_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST api/reviews
        [HttpPost, Authorize]
        public IActionResult Post([FromBody] Review data)
        {
            try
            {
                string userId = User.FindFirstValue("id");

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }
                data.UserId = userId;

                _context.Reviews.Add(data);
                _context.SaveChanges();
                return StatusCode(201, data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}

