using Microsoft.AspNetCore.Mvc.RazorPages;
using PMLWebApp.Models;
using PMLWebApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMLWebApp.Pages.PML
{
    public class IndexModel : PageModel
    {
        private readonly MongoDbService _mongoDbService;

        public IndexModel(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        public List<Player> Players { get; set; } = new List<Player>();

        public async Task OnGetAsync(string searchQuery = null)
        {
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                Players = await _mongoDbService.GetAllPlayers(); 
            }
            else
            {
                Players = _mongoDbService.SearchPlayers(searchQuery); 
            }
        }
    }
}
