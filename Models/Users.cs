using Microsoft.AspNetCore.Identity;

namespace ASPNETMVC.Models
{
    public class Users : IdentityUser
    {
        public string? Name { get; set; }
        public string? EmpGender { get; set; }

        public string? ImageUrl {get; set;}
    }
}

