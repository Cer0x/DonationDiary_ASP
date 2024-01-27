using Microsoft.AspNetCore.Identity;

namespace DonationDiary_ASP.Models
{
    public class User:IdentityUser<int>
    {
        public User_Details UserDetails { get; set; }
        public ICollection<user_entries> user_Entries { get; set; }
    }
}
