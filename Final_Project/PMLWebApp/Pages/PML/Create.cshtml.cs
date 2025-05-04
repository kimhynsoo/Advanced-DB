using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PMLWebApp.Models;
using PMLWebApp.Services;
using System.Threading.Tasks;

namespace PMLWebApp.Pages.PML
{
    public class CreateModel : PageModel
    {
        private readonly MongoDbService _mongoDbService;

        public CreateModel(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        [BindProperty]
        public Player Player { get; set; }

        public void OnGet()
        {
            // GET 요청 시 아무것도 하지 않음
        }

        public async Task<IActionResult> OnPostAsync()
        {
            
            _mongoDbService.AddPlayer(Player);
            return RedirectToPage("./Index");
        }

    }
}
