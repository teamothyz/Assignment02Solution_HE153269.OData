using eBookStoreApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eBookStoreApp.Pages.Author
{
    [Authorize(Roles = "admin")]
    public class EditModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public BusinessObject.Models.Author Author { get; set; } = null!;

        [BindProperty]
        public BusinessObject.Models.Author AuthorUpdate { get; set; } = null!;

        public async Task<IActionResult> OnGet()
        {
            var client = new ClientService(HttpContext);
            var author = await client.Get<BusinessObject.Models.Author>($"/odata/authors/{Id}");
            if (author == null) return NotFound();

            Author = author;
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var client = new ClientService(HttpContext);
            var rs = await client.Patch($"/odata/authors/{Id}", AuthorUpdate);
            if (rs == null) return NotFound();

            return RedirectToPage("/Author/View", new { AuthorUpdate.Id });
        }
    }
}
