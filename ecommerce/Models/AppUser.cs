using Microsoft.AspNetCore.Identity;

namespace ecommerce.Models
{
    public class AppUser : IdentityUser
    {
        public string Nome { get; set;}
    }
}