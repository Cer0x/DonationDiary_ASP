using DonationDiary_ASP.Models;

namespace DonationDiary_ASP.Views.ViewModels
{
    public class EntryViewModel
    {
        public int BloodAmount { get; set; }
        public string DateOfDonation { get; set; }
        public string Comment { get; set; }
        public ICollection<user_entries> user_Entries { get; set; }



    }
}
