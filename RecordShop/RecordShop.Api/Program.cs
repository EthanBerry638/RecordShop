using Microsoft.EntityFrameworkCore;
using RecordShop.Api.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RecordShopContext>(options =>
         options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();