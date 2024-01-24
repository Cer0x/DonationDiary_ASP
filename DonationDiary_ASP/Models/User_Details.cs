namespace DonationDiary_ASP.Models
{

    public class User_Details
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public string? Phone { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }  

    }
}
