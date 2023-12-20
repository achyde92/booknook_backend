using System;
namespace FullStackAuth_WebAPI.DataTransferObjects
{
	public class ReviewWithUserDTO
	{
		public UserForDisplayDto Users { get; set; }
		public string Text { get; set; }
		public double Rating { get; set; }
		public string BookId { get; set; }
		public int ReviewId { get; set; }
	}
}

