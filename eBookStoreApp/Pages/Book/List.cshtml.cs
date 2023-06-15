using eBookStoreApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eBookStoreApp.Pages.Book
{
    [Authorize]
    public class ListModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string? Title { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? LowPrice { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? HighPrice { get; set; }

        public List<BusinessObject.Models.Book> Books { get; set; } = new();

        public async Task<IActionResult> OnGet()
        {
            var url = "/odata/books?$expand=publisher";
            var conditions = new List<string>();
            if (Title != null) conditions.Add($"contains(Title, '{Title}')");
            if (LowPrice != null) conditions.Add($"Price ge {LowPrice}");
            if (HighPrice != null) conditions.Add($"Price le {HighPrice}");
            if (conditions.Count > 0) url += $"&$filter={string.Join(" and ", conditions)}";

            var client = new ClientService(HttpContext);
            var books = await client.Get<OdataList<BusinessObject.Models.Book>>(url);
            if (books == null) return NotFound();
            Books = books.Value;
            return Page();
        }
    }
}
