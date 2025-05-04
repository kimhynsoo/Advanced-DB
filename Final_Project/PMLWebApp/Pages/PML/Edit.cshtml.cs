using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PMLWebApp.Models;
using PMLWebApp.Services;
using System.Threading.Tasks;

namespace PMLWebApp.Pages.PML
{
    public class EditModel : PageModel
    {
        private readonly MongoDbService _mongoDbService;

        public EditModel(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        [BindProperty]
        public Player Player { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            Player = await _mongoDbService.GetPlayerById(id);
            if (Player == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page(); 
            }

            
            _mongoDbService.UpdatePlayer(Player.Player_ID, Player);
            return RedirectToPage("./Index"); 
        }
    }
}
