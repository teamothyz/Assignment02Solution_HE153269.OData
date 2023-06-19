using BusinessObject.API.Response;
using eBookStoreApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eBookStoreApp.Pages.User
{
    [Authorize(Roles = "member")]
    public class IndexModel : PageModel
    {
        public GetInfoResponseModel UserInfo { get; set; } = null!;

        public async Task<IActionResult> OnGet()
        {
            var client = new ClientService(HttpContext);
            var res = await client.Get<GetInfoResponseModel>("/api/auth/user");
            if (res == null) return NotFound();

            UserInfo = res;
            return Page();
        }
    }
}
