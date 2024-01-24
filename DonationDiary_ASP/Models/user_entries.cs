namespace DonationDiary_ASP.Models
{
    public class user_entries
    {

        public int UserId { get; set; }
        public User User { get; set; }

        public int EntryId { get; set; }
        public Entry Entry { get; set; }


    }
}
