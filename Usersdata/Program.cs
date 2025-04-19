using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Usersdata.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Database Connection
builder.Services.AddDbContext<UsersContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // sql appsettings.json dan olur
// CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocal", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // Yaln?z yerli sayta icaz? verin
              .AllowAnyMethod()                  // ?st?nil?n HTTP metoduna icaz? verin
              .AllowAnyHeader()                // ?st?nil?n HTTP metoduna icaz? verin
              .AllowCredentials();                 // Etibarnam?l?ri q?bul edin
    });
});



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// statik fayllar üçün middleware 
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images")),
    RequestPath = "/images" 
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable CORS with the defined policy
app.UseCors("AllowLocal");



app.UseAuthorization();

app.MapControllers();

app.Run();
