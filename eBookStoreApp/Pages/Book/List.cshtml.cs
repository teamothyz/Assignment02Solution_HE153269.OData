using eBookStoreApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eBookStoreApp.Pages.Book
{
    [Authorize]
    public class ListModel : PageModel
    {
        public List<BusinessObject.Models.Book> Books { get; set; } = new();
        [BindProperty(SupportsGet = true)]

        public int Id { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var client = new ClientService(HttpContext);
            var books = await client.Get<OdataList<BusinessObject.Models.Book>>("/odata/books?$expand=publisher");
            if (books?.Value.Any() != true) return NotFound();
            Books = books.Value;
            return Page();
        }

        public async Task<IActionResult> OnPostDelete()
        {
            var client = new ClientService(HttpContext);
            var res = await client.Delete($"/odata/books/{Id}");
            if (res == null) return NotFound();

            else return RedirectToPage("/Book/List");
        }
    }
}
