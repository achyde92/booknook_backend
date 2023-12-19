using System;
namespace FullStackAuth_WebAPI.DataTransferObjects
{
	public class BookDetailsDTO
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string ThumbnailUrl { get; set; }
        public ReviewWithUserDTO User { get; set; }
    }
}

