using BusinessObject.API.Response;
using eBookStoreApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eBookStoreApp.Pages.User
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public BusinessObject.Models.User UserUpdate { get; set; } = new();

        public GetInfoResponseModel UserInfo { get; set; } = null!;

        public async Task<IActionResult> OnGet()
        {
            var client = new ClientService(HttpContext);
            var res = await client.Get<GetInfoResponseModel>("/api/auth/user");
            if (res == null) return NotFound();

            UserInfo = res;
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            UserUpdate.HireDate = UserUpdate.HireDate.ToUniversalTime();
            var client = new ClientService(HttpContext);
            var res = await client.Patch("/odata/users", UserUpdate);
            if (res == null) return BadRequest();

            return RedirectToPage("/User/Index");
        }
    }
}
