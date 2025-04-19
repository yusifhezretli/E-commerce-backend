using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Usersdata.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Database Connection
builder.Services.AddDbContext<UsersContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Ba?lant? dizesi `appsettings.json`'dan al?n?r

// CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocal", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // Sadece yerel siteye izin ver
              .AllowAnyMethod()                    // Herhangi bir HTTP metoduna izin ver
              .AllowAnyHeader()                    // Herhangi bir HTTP ba?l???na izin ver
              .AllowCredentials();                 // Kimlik bilgilerini kabul et
    });
});

// Add Session Support
builder.Services.AddDistributedMemoryCache(); // Oturum verileri i�in bellek kullan?m?
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Oturum s�resi (30 dakika)
    options.Cookie.HttpOnly = true; // �erezlerin yaln?zca HTTP �zerinden eri?ilebilir olmas?
    options.Cookie.IsEssential = true; // �erezlerin zorunlu oldu?unu belirtir
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Statik dosyalar?n sunulabilmesi i�in middleware ekleyin
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images")),
    RequestPath = "/images"  // URL �zerinden "/images" olarak eri?ilecektir
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

// Add Session Middleware
app.UseSession(); // Oturum y�netimi i�in Session Middleware ekleniyor

app.UseAuthorization();

app.MapControllers();

app.Run();
