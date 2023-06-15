using eBookStoreApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eBookStoreApp.Pages.Author
{
    [Authorize]
    public class ListModel : PageModel
    {
        public List<BusinessObject.Models.Author> Authors { get; set; } = new();

        public async Task<IActionResult> OnGet()
        {
            var client = new ClientService(HttpContext);
            var authors = await client.Get<OdataList<BusinessObject.Models.Author>>("/odata/authors");
            if (authors == null) return NotFound();

            Authors = authors.Value;
            return Page();
        }
    }
}
