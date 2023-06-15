using eBookStoreApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eBookStoreApp.Pages.Author
{
    [Authorize(Roles = "admin")]
    public class AddModel : PageModel
    {
        [BindProperty]
        public BusinessObject.Models.Author Author { get; set; } = null!;

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var client = new ClientService(HttpContext);
            var author = await client.Post<BusinessObject.Models.Author>($"/odata/authors", Author);
            if (author == null) return NotFound();
            return RedirectToPage("/Author/View", new { author.Id });
        }
    }
}
