// Controllers/PlayersController.cs
using Microsoft.AspNetCore.Mvc;
using PMLWebApp.Models;
using PMLWebApp.Services;

namespace PMLWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly MongoDbService _mongoDbService;

        public PlayersController(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        // 1. 모든 선수 가져오기
        
        [HttpGet]
        public async Task<ActionResult<List<Player>>> GetAllPlayers()
        {
            var players = await _mongoDbService.GetAllPlayers();
            return Ok(players); // ActionResult로 변환
        }
        
        // 2. 선수 ID로 선수 가져오기
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayerById(string id)
        {
            var player = await _mongoDbService.GetPlayerById(id);
            if (player == null)
            {
                return NotFound(); 
            }
            return Ok(player); 
        }

        // 3. 텍스트 검색
        [HttpGet("search")]
        public ActionResult<List<Player>> Search(string query)
        {
            var players = _mongoDbService.SearchPlayers(query);
            return players;
        }

        // 4. 선수 추가
        [HttpPost]
public ActionResult<Player> Create(Player player)
        {
            
            _mongoDbService.AddPlayer(player);
            return CreatedAtAction(nameof(GetPlayerById), new { id = player.Id.ToString() }, player);
        }

        // 5. 선수 수정
        [HttpPut("{id}")]
        public IActionResult Update(string id, Player playerIn)
        {
            var player = _mongoDbService.GetPlayerById(id);
            if (player == null) return NotFound();

            _mongoDbService.UpdatePlayer(id, playerIn);
            return NoContent();
        }

        // 6. 선수 삭제
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var player = _mongoDbService.GetPlayerById(id);
            if (player == null) return NotFound();

            _mongoDbService.DeletePlayer(id);
            return NoContent();
        }
    }
}
