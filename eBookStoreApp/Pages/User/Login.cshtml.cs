using BusinessObject.API.Request;
using BusinessObject.API.Response;
using eBookStoreApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace eBookStoreApp.Pages.User
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Error { get; set; } = null!;

        [BindProperty]
        public string Email { get; set; } = null!;

        [BindProperty]
        public string Password { get; set; } = null!;

        public IActionResult OnGet()
        {
            if (User.Identity?.IsAuthenticated == true) return RedirectToPage("/Index");
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (User.Identity?.IsAuthenticated == true) return RedirectToPage("/User/Index");

            var client = new ClientService(HttpContext);
            var requestModel = new LoginRequestModel { Email = Email, Password = Password };
            var res = await client.Post<LoginResponseModel>("/api/auth/login", requestModel);
            if (res == null) return RedirectToPage("/User/Login", new { Error = "Login Failed!" });
            HttpContext.Response.Cookies.Append("AccessToken", res.Token, new CookieOptions
            {
                Expires= DateTime.Now.AddDays(1).AddMinutes(-1)
            });
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, res.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{res.FirstName} {res.MiddleName} {res.LastName}"),
                new Claim(ClaimTypes.Email, res.Email),
                new Claim(ClaimTypes.Role, res.Role)
            };
            var claimsIdentity = new ClaimsIdentity(claims, "login");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync("CookieAuthentication", claimsPrincipal, new AuthenticationProperties
            {
                IsPersistent = false
            });
            return RedirectToPage("/User/Index");
        }
    }
}
