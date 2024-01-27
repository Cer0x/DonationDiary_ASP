namespace DonationDiary_ASP.Models
{

    public class User_Details
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Phone { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }  

    }
}
