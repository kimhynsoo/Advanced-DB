[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly MongoDBService _mongoDBService;

    public ProductController(MongoDBService mongoDBService)
    {
        _mongoDBService = mongoDBService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _mongoDBService.GetAllAsync());

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] BsonDocument document)
    {
        await _mongoDBService.AddAsync(document);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] BsonDocument document)
    {
        await _mongoDBService.UpdateAsync(id, document);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _mongoDBService.DeleteAsync(id);
        return Ok();
    }
}