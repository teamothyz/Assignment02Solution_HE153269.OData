using eBookStoreApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eBookStoreApp.Pages.Book
{
    [Authorize]
    public class ViewModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public BusinessObject.Models.Book Book { get; set; } = null!;

        public async Task<IActionResult> OnGet()
        {
            var client = new ClientService(HttpContext);
            var book = await client.Get<BusinessObject.Models.Book>($"/odata/books/{Id}?$expand=publisher");
            if (book == null) return NotFound();

            Book = book;
            return Page();
        }
    }
}
