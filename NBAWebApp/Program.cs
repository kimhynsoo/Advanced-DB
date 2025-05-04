using Microsoft.AspNetCore.Builder;
using PMLWebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(); // Razor Pages 사용

// MongoDB 서비스 등록
builder.Services.AddSingleton<MongoDbService>();
builder.Services.AddControllers(); // API 컨트롤러 사용
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

// API 컨트롤러 매핑 (필수)
app.MapControllers(); // API 컨트롤러 등록
app.MapRazorPages(); // Razor Pages 등록

app.Run();
