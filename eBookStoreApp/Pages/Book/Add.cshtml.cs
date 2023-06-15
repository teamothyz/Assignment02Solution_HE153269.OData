using eBookStoreApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eBookStoreApp.Pages.Book
{
    public class AddModel : PageModel
    {
        [BindProperty]
        public BusinessObject.Models.Book Book { get; set; } = null!;

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            Book.PulishedDate = Book.PulishedDate.ToUniversalTime();
            var client = new ClientService(HttpContext);
            var book = await client.Post<BusinessObject.Models.Book>("odata/books", Book);
            if (book == null) return BadRequest();

            return RedirectToPage("/Book/View", new { book.Id });
        }
    }
}
