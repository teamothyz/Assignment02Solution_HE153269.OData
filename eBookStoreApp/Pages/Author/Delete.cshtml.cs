using eBookStoreApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eBookStoreApp.Pages.Author
{
    [Authorize(Roles = "admin")]
    public class DeleteModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public async Task<IActionResult> OnPostDelete()
        {
            var client = new ClientService(HttpContext);
            var res = await client.Delete($"/odata/authors/{Id}");
            if (res == null) return NotFound();

            return RedirectToPage("/Author/List");
        }
    }
}
