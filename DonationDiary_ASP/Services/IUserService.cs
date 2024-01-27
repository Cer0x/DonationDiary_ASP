using Microsoft.AspNetCore.Identity;
using DonationDiary_ASP.Views.ViewModels;
namespace DonationDiary_ASP.Services
{
    public interface IUserService
    {
        string GetUserId();
        Task<IdentityResult> ChangePasswordAsync(ChangePasswordViewModel changepasswordviewmodel);


    }
}
