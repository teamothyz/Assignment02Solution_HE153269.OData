using eBookStoreApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eBookStoreApp.Pages.Publisher
{
    [Authorize]
    public class ListModel : PageModel
    {
        public List<BusinessObject.Models.Publisher> Publishers { get; set; } = new();

        public async Task<IActionResult> OnGet()
        {
            var client = new ClientService(HttpContext);
            var publishers = await client.Get<OdataList<BusinessObject.Models.Publisher>>("/odata/publishers");
            if (publishers == null) return NotFound();
            Publishers = publishers.Value;
            return Page();
        }
    }
}
