namespace DonationDiary_ASP.Models
{
    public class Entry
    {
        public int Id { get; set; }
        public required string DateOfDonation { get; set; }
        public required int BloodAmount { get; set; }
        public string? Comment { get; set; }

        public ICollection<user_entries> user_Entries { get; set; }

    }
}
