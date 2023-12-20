using FullStackAuth_WebAPI.DataTransferObjects;
using Microsoft.AspNetCore.Identity;

namespace FullStackAuth_WebAPI.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        internal List<UserForDisplayDto> Select(Func<object, UserForDisplayDto> value)
        {
            throw new NotImplementedException();
        }
    }
}
