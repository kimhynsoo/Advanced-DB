using Microsoft.AspNetCore.Mvc;
using PMLWebApp.Models;
using PMLWebApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMLWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly MongoDbService _mongoDbService;

        public TeamsController(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        // 1. 모든 팀 가져오기
        [HttpGet]
        public async Task<ActionResult<List<Team>>> Get()
        {
            var teams = await _mongoDbService.GetAllTeams();
            return Ok(teams); 
        }

        // 2. 팀 ID로 팀 가져오기
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> Get(string id)
        {
            var team = await _mongoDbService.GetTeamById(id);
            if (team == null) return NotFound();
            return Ok(team); // 팀 정보 반환
        }

        // 3. 팀 추가
        [HttpPost]
        public async Task<ActionResult<Team>> Create(Team team)
        {
            await _mongoDbService.AddTeam(team); 
            return CreatedAtAction(nameof(Get), new { id = team.Team_ID }, team); 
        }

        // 4. 팀 수정
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Team teamIn)
        {
            var team = await _mongoDbService.GetTeamById(id);
            if (team == null) return NotFound();

            await _mongoDbService.UpdateTeam(id, teamIn);
            return NoContent();
        }

        // 5. 팀 삭제
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var team = await _mongoDbService.GetTeamById(id);
            if (team == null) return NotFound();

            await _mongoDbService.DeleteTeam(id); 
            return NoContent();
        }
    }
}
