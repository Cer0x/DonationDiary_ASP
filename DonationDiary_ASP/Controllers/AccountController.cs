using DonationDiary_ASP.Data;
using DonationDiary_ASP.Models;
using DonationDiary_ASP.Services;
using DonationDiary_ASP.Views.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DonationDiary_ASP.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDbContext context, IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _userService = userService;
        }
        public async Task<IActionResult> AllUsers()
        {
            var user = await _context.Users.ToListAsync();
            return View(user);
        }

        public async Task<IActionResult> Search(string search)
        {
            var users = await _context.Users.ToListAsync();
            if (!string.IsNullOrEmpty(search))
            {
                var filter = users.Where(u => u.UserName.Contains(search));
                return View("AllUsers", filter);
            }
            return View("AllUsers", users);
        }

        // LOGGING IN
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }
            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);
            if (user != null)
            {
                var checkPass = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (checkPass)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Entries");
                    }
                }
                TempData["Error"] = "Please try again!";
                return View(loginViewModel);
            }
            TempData["Error"] = "Please try again!";
            return View(loginViewModel);
        }

        // REGISTRATION
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerViewModel);
            }

            var newUser = new User()
            {
                UserDetails = new User_Details()
                {
                    Name = registerViewModel.Name,
                    Surname = registerViewModel.Surname,
                    Phone = registerViewModel.PhoneNumber
                },
                Email = registerViewModel.EmailAddress,
                UserName = registerViewModel.UserName,
            };
            
            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);
            if (!newUserResponse.Succeeded)
            {
                foreach (var error in newUserResponse.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(registerViewModel);
            }
            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, Role.User);
            }

            return View("RegisterCompleted");
        }
        public IActionResult ChangePassword()
        {
            return View(new ChangePasswordViewModel());
        }


        // PASSWD CHANGE
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var change = await _userService.ChangePasswordAsync(model);
                if (change.Succeeded)
                {
                    ModelState.Clear();
                    TempData["Success"] = "Password changed";
                    return View();
                }

                TempData["Error"] = "Wrong password";
            }
            return View(model);
        }
    }
}
