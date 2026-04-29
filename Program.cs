using Microsoft.EntityFrameworkCore;
using StockMarketAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// Đăng ký DbContext với Connection String từ appsettings
builder.Services.AddDbContext<StockMarketContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("StockMarketDb"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("StockMarketDb"))
    ));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "StockMarket API", Version = "v1" });
});

// Cấu hình CORS cho Angular (quan trọng)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.AllowAnyOrigin()           // Cho phép Angular gọi từ localhost:4200
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAngular");   // Sử dụng CORS

app.UseAuthorization();

app.MapControllers();

app.Run();
