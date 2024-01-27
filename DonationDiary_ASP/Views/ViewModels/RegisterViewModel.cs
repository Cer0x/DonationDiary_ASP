using System.ComponentModel.DataAnnotations;

namespace DonationDiary_ASP.Views.ViewModels
{
    public class RegisterViewModel
    {
        public string UserName { get; set; }
        public string Name { get; set; }

        public string Surname { get; set; }

        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        
        
    }
}
