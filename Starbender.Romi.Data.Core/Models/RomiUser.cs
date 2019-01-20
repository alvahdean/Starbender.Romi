namespace Starbender.Romi.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class RomiUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}