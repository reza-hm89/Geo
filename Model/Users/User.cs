
using Microsoft.AspNetCore.Identity;

namespace Model.Users
{
    public class User : IdentityUser
    {
        public string Password { get; set; }
    }
}
