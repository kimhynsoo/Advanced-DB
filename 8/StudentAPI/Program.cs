using StudentAPI.Models;
using StudentAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<StudentDatabaseSettings>(
    builder.Configuration.GetSection("StudentDatabase"));

builder.Services.AddSingleton<StudentsService>();

builder.Services.AddControllers()
    .AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();