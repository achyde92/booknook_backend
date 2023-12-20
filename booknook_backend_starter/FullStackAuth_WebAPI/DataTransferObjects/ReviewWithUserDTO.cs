using System;
namespace FullStackAuth_WebAPI.DataTransferObjects
{
	public class ReviewWithUserDTO
	{
		public List<UserForDisplayDto> Users { get; set; }
		public string Text { get; set; }
		public double Rating { get; set; }
	}
}

