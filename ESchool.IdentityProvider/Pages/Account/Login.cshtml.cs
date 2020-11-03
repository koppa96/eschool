using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ESchool.IdentityProvider.Domain.Entities.Users;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ESchool.IdentityProvider.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IIdentityServerInteractionService interactionService;
        private readonly IUserClaimsPrincipalFactory<User> claimsPrincipalFactory;
        private readonly UserManager<User> userManager;

        [Required(ErrorMessage = "Kötelező")]
        [BindProperty]
        public string Username { get; set; } = "";

        [Required(ErrorMessage = "Kötelező")]
        [BindProperty]
        public string Password { get; set; } = "";

        [BindProperty]
        public bool RememberMe { get; set; } = false;

        [BindProperty]
        public string ReturnUrl { get; set; } = "";

        public List<string> Errors { get; set; } = new List<string>();

        public LoginModel(
            IIdentityServerInteractionService interactionService,
            IUserClaimsPrincipalFactory<User> claimsPrincipalFactory,
            UserManager<User> userManager)
        {
            this.interactionService = interactionService;
            this.claimsPrincipalFactory = claimsPrincipalFactory;
            this.userManager = userManager;
        }

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string action)
        {
            if (action == "register")
            {
                return RedirectToPage("/Account/Register", new { returnUrl = Request.GetEncodedUrl() });
            }

            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(Username);
                if (user != null && await userManager.CheckPasswordAsync(user, Password))
                {
                    var signInProperties = new AuthenticationProperties
                    {
                        ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1),
                        AllowRefresh = true,
                        RedirectUri = ReturnUrl,
                        IsPersistent = RememberMe
                    };

                    var claimsPrincipal = await claimsPrincipalFactory.CreateAsync(user);
                    await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, claimsPrincipal, signInProperties);
                    HttpContext.User = claimsPrincipal;

                    if (interactionService.IsValidReturnUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                }
                else
                {
                    Errors.Add("Hibás felhasználónév vagy jelszó!");
                    return Page();
                }
            }

            return Page();
        }
    }
}