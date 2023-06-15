using eBookStoreApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eBookStoreApp.Pages.Book
{
    [Authorize(Roles = "admin")]
    public class DeleteModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public async Task<IActionResult> OnPost()
        {
            var client = new ClientService(HttpContext);
            var res = await client.Delete($"/odata/books/{Id}");
            if (res == null) return NotFound();

            else return RedirectToPage("/Book/List");
        }
    }
}
