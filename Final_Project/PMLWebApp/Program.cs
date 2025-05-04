using Microsoft.AspNetCore.Builder;
using PMLWebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(); // Razor Pages 사용

// MongoDB 서비스 등록
builder.Services.AddSingleton<MongoDbService>();
builder.Services.AddControllers(); 
// Swagger 서비스 추가
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Swagger 미들웨어 활성화
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers(); 
app.MapRazorPages(); 

app.MapGet("/", async context =>
{
    context.Response.Redirect("/PML"); 
});

app.Run();
