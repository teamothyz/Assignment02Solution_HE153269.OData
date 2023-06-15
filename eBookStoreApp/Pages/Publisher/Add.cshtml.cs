using eBookStoreApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eBookStoreApp.Pages.Publisher
{
    [Authorize(Roles = "admin")]
    public class AddModel : PageModel
    {
        [BindProperty]
        public BusinessObject.Models.Publisher Publisher { get; set; } = null!;

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var client = new ClientService(HttpContext);
            var book = await client.Post<BusinessObject.Models.Publisher>("odata/publishers", Publisher);
            if (book == null) return BadRequest();

            return RedirectToPage("/Publisher/View", new { book.Id });
        }
    }
}
