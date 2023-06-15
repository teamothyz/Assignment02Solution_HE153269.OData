using eBookStoreApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eBookStoreApp.Pages.Publisher
{
    public class DeleteModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public async Task<IActionResult> OnPost()
        {
            var client = new ClientService(HttpContext);
            var res = await client.Delete($"/odata/publishers/{Id}");
            if (res == null) return NotFound();

            else return RedirectToPage("/Publisher/List");
        }
    }
}
