using eBookStoreApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eBookStoreApp.Pages.Publisher
{
    [Authorize(Roles = "admin")]
    public class EditModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty]
        public BusinessObject.Models.Publisher PublisherUpdate { get; set; } = new();

        public BusinessObject.Models.Publisher Publisher { get; set; } = new();

        public async Task<IActionResult> OnGet()
        {
            var client = new ClientService(HttpContext);
            var publisher = await client.Get<BusinessObject.Models.Publisher>($"/odata/publishers/{Id}");
            if (publisher == null) return NotFound();
            Publisher = publisher;
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var client = new ClientService(HttpContext);
            var response = await client.Patch($"/odata/publishers/{Id}", PublisherUpdate);
            if (response == null) return BadRequest();
            return RedirectToPage("/Publisher/View", new { PublisherUpdate.Id });
        }
    }
}
