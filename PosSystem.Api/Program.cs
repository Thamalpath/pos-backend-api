using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add API Controllers & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS configuration for Web POS & Admin Web
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowPosClients", policy =>
    {
        policy.WithOrigins("http://localhost:1420", "http://localhost:3000", "tauri://localhost")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// PostgreSQL with Entity Framework Core
// builder.Services.AddDbContext<PosDbContext>(options =>
//     options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConnection")));

// Redis Distributed Cache for session & fast stock lock
// builder.Services.AddStackExchangeRedisCache(options =>
// {
//     options.Configuration = builder.Configuration.GetConnectionString("Redis");
// });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowPosClients");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
