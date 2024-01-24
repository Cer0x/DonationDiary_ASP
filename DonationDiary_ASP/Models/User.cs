using Microsoft.AspNetCore.Identity;

namespace DonationDiary_ASP.Models
{
    public class User:IdentityUser<int>
    {
        public required int Id { get; set; }
        public required string Email { get; set; }

        public required string Password { get; set; }

        public required bool IsAdmin { get; set; }

        public User_Details UserDetails { get; set; }
        public ICollection<user_entries> user_Entries { get; set; }



    }
}
