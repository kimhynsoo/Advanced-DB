using MongoDB.Driver;
using PMLWebApp.Models;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMLWebApp.Services
{
    public class MongoDbService
    {
        private readonly IMongoCollection<Team> _teams;
        private readonly IMongoCollection<Player> _players;

        public MongoDbService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("MongoDb"));
            var database = client.GetDatabase("PL_SOCCER");

            _teams = database.GetCollection<Team>("Teams");
            _players = database.GetCollection<Player>("Players");
        }

        // Player 관련 메서드

        // 1. 모든 선수 가져오기
        public async Task<List<Player>> GetAllPlayers()
        {
            return await _players.Find(new BsonDocument()).ToListAsync(); // 플레이어 컬렉션에서 모든 문서 가져오기
        }

        // 2. 선수 ID로 선수 가져오기
        public async Task<Player> GetPlayerById(string id)
        {
            var filter = Builders<Player>.Filter.Eq(player => player.Player_ID, id); // PlayerId가 id와 같은 플레이어 찾기
            return await _players.Find(filter).FirstOrDefaultAsync(); // 비동기적으로 첫 번째 플레이어 반환
        }

        // 3. 텍스트 검색 (이름 또는 팀 이름)
        public List<Player> SearchPlayers(string searchText)
        {
            var filter = Builders<Player>.Filter.Text(searchText);
            return _players.Find(filter).ToList();
        }

        // 4. 선수 추가
        public async Task AddPlayer(Player player) => await _players.InsertOneAsync(player);

        // 5. 선수 수정
        public async Task UpdatePlayer(string id, Player playerIn) => await _players.ReplaceOneAsync(player => player.Player_ID == id, playerIn);

        // 6. 선수 삭제
        public async Task DeletePlayer(string id) => await _players.DeleteOneAsync(player => player.Player_ID == id);

        // Team 관련 메서드

        public async Task<List<Team>> GetAllTeams() => await _teams.Find(team => true).ToListAsync();

        public async Task<Team> GetTeamById(string id)
        {
            var filter = Builders<Team>.Filter.Eq(team => team.Team_ID, id);
            return await _teams.Find(filter).FirstOrDefaultAsync();
        }

        // 3. 팀 추가
        public async Task AddTeam(Team team) => await _teams.InsertOneAsync(team);

        // 4. 팀 수정
        public async Task UpdateTeam(string id, Team teamIn) => await _teams.ReplaceOneAsync(team => team.Team_ID == id, teamIn);

        // 5. 팀 삭제
        public async Task DeleteTeam(string id) => await _teams.DeleteOneAsync(team => team.Team_ID == id);
    }
}
