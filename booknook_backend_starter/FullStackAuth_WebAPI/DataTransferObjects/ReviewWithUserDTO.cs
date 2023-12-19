using System;
namespace FullStackAuth_WebAPI.DataTransferObjects
{
	public class ReviewWithUserDTO
	{
		public string UserId { get; set; }
		public string Text { get; set; }
		public double Rating { get; set; }
	}
}

