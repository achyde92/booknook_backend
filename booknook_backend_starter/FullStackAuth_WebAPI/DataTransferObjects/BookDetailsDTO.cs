using System;
namespace FullStackAuth_WebAPI.DataTransferObjects
{
	public class BookDetailsDTO
    {
        public string BookId { get; set; }
        public string Title { get; set; }
        public string ThumbnailUrl { get; set; }
        public List<ReviewWithUserDTO> Reviews { get; set; }
        public double AverageRating { get; set; }
        public bool IsFavorited { get; set; }
    }
}

