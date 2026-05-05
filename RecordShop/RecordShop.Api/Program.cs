using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RecordShop.Api.Data;
using RecordShop.Api.Middleware;
using RecordShop.Api.Repositories;
using RecordShop.Api.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

var keepAliveConnection = new SqliteConnection(connectionString);
keepAliveConnection.Open();

builder.Services.AddDbContext<RecordShopContext>(options =>
         options.UseSqlite(keepAliveConnection));

builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();
builder.Services.AddScoped<IAlbumService, AlbumService>();

builder.Services.AddTransient<ExceptionMiddleware>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<RecordShopContext>();
    dbContext.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();