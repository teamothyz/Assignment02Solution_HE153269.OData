using BusinessObject.API.Response;
using eBookStoreApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eBookStoreApp.Pages.User
{
    [Authorize(Roles = "member")]
    [IgnoreAntiforgeryToken(Order = 2000)]
    public class EditModel : PageModel
    {
        [BindProperty]
        public BusinessObject.Models.User UserUpdate { get; set; } = new();

        public GetInfoResponseModel UserInfo { get; set; } = null!;
        public List<BusinessObject.Models.Publisher> Publishers { get; set; } = new();

        public async Task<IActionResult> OnGet()
        {
            var client = new ClientService(HttpContext);
            var res = await client.Get<GetInfoResponseModel>("/api/auth/user");
            if (res == null) return NotFound();

            var publishers = await client.Get<OdataList<BusinessObject.Models.Publisher>>("/odata/publishers");
            if (publishers == null) return NotFound();
            Publishers = publishers.Value;

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
