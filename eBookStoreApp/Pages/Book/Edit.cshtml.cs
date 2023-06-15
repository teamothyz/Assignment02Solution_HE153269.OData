using eBookStoreApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eBookStoreApp.Pages.Book
{
    [Authorize(Roles = "admin")]
    public class EditModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public BusinessObject.Models.Book Book { get; set; } = null!;

        [BindProperty]
        public BusinessObject.Models.Book UpdateBook { get; set; } = null!;

        public List<BusinessObject.Models.Publisher> Publishers { get; set; } = new();

        public async Task<IActionResult> OnGet()
        {
            var client = new ClientService(HttpContext);
            var book = await client.Get<BusinessObject.Models.Book>($"/odata/books/{Id}?$expand=publisher");
            if (book == null) return NotFound();

            var publishers = await client.Get<OdataList<BusinessObject.Models.Publisher>>("/odata/publishers");
            if (publishers == null) return NotFound();
            Publishers = publishers.Value;

            Book = book;
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            UpdateBook.PulishedDate = UpdateBook.PulishedDate.ToUniversalTime();
            var client = new ClientService(HttpContext);
            var response = await client.Patch($"/odata/books/{Id}", UpdateBook);
            if (response == null) return BadRequest();
            return RedirectToPage("/Book/View", new { UpdateBook.Id });
        }
    }
}
