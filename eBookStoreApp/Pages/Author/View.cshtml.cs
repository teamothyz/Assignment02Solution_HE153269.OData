using BusinessObject.Models;
using eBookStoreApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eBookStoreApp.Pages.Author
{
    [Authorize]
    public class ViewModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public BusinessObject.Models.Author Author { get; set; } = null!;

        public async Task<IActionResult> OnGet()
        {
            var client = new ClientService(HttpContext);
            var author = await client.Get<BusinessObject.Models.Author>($"/odata/authors/{Id}");
            if (author == null) return NotFound();

            Author = author;
            return Page();
        }
    }
}
