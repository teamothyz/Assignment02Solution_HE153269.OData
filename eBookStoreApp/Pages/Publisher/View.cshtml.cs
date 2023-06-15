using eBookStoreApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eBookStoreApp.Pages.Publisher
{
    [Authorize]
    public class ViewModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public BusinessObject.Models.Publisher Publisher { get; set; } = new();

        public async Task<IActionResult> OnGet()
        {
            var client = new ClientService(HttpContext);
            var publisher = await client.Get<BusinessObject.Models.Publisher>($"/odata/publishers/{Id}");
            if (publisher == null) return NotFound();
            Publisher = publisher;
            return Page();
        }
    }
}
