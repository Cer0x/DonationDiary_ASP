using DonationDiary_ASP.Models;
using DonationDiary_ASP.Views.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace DonationDiary_ASP.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserManager<User> _userManager;

        public UserService(IHttpContextAccessor httpContext, UserManager<User> userManager)
        {
            _httpContext = httpContext;
            _userManager = userManager;
        }

        public string? GetUserIdAsync()
        {
            return _httpContext.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordViewModel changePasswordViewModel)
        {
            var userId = GetUserIdAsync();
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.ChangePasswordAsync(user, changePasswordViewModel.CurrentPassword, changePasswordViewModel.NewPassword);
        }


    }
}
