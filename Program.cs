using LibraryService.WebAPI.Data;
using LibraryService.WebAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Register EF Core DbContext with In-Memory provider or your chosen provider
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseInMemoryDatabase("librarydb"));

// Register your services that depend on LibraryContext
builder.Services.AddScoped<IBooksService, BooksService>();
builder.Services.AddScoped<ILibrariesService, LibrariesService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
