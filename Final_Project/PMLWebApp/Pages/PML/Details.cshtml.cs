using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PMLWebApp.Models;
using PMLWebApp.Services;
using System.Threading.Tasks;

namespace PMLWebApp.Pages.PML
{
    public class DetailsModel : PageModel
    {
        private readonly MongoDbService _mongoDbService;

        public DetailsModel(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            await _mongoDbService.DeletePlayer(id); 
            return RedirectToPage("/PML/Index"); 
        }

        public Player Player { get; set; }
        public Team Team { get; set; } 

        public async Task<IActionResult> OnGetAsync(string id)
        {
            Player = await _mongoDbService.GetPlayerById(id);
            
            if (Player == null)
            {
                return NotFound();
            }


            Team = await _mongoDbService.GetTeamById(Player.Team_ID);

            return Page();
        }
    }
}
