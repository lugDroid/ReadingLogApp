using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReadingLog.Data;

namespace ReadingLog.App.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberLogin { get; set; }
        public string ReturnUrl { get; set; }

        public LoginModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult OnGet(string returnUrl = "/")
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            var user = _userRepository.GetByUsernameAndPassword(Username, Password);

            if (user == null)
                return Unauthorized();
                
            return Page();
        }
    }
}