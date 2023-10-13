namespace DonationDiary_ASP.Models
{
    public class Entry
    {
        public int Id { get; set; }
        public string DateOfDonation { get; set; }
        public int BloodAmount { get; set; }
        public string? Comment { get; set; }

        public Entry()
        {
            
        }

    }
}
