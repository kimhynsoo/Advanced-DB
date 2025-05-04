using MongoDB.Driver;
using MongoDB.Bson;

public class MongoDBService
{
    private readonly IMongoCollection<BsonDocument> _collection;

    public MongoDBService(IConfiguration config)
    {
        var client = new MongoClient(config["MongoDB:ConnectionString"]);
        var database = client.GetDatabase(config["MongoDB:DatabaseName"]);
        _collection = database.GetCollection<BsonDocument>("courses");
    }

    public async Task<List<BsonDocument>> GetAllAsync() => await _collection.Find(new BsonDocument()).ToListAsync();
    public async Task AddAsync(BsonDocument document) => await _collection.InsertOneAsync(document);
    public async Task UpdateAsync(string id, BsonDocument document) =>
        await _collection.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(id)), document);
    public async Task DeleteAsync(string id) =>
        await _collection.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
}