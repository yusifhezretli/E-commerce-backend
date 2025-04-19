using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Usersdata.Models;
using Microsoft.Extensions.Configuration;
using System;

namespace Usersdata.Controllers
{
    // API controller üçün əsas yol
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        // IConfiguration və UsersContext servisləri
        private readonly IConfiguration _configuration;
        private readonly UsersContext _context;

        // Konstruktor: IConfiguration və UsersContext-i inject etmək
        public AuthenticationController(IConfiguration configuration, UsersContext context)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // Login API
        [HttpPost("login")]
        public IActionResult Login([FromBody] User loginRequest)
        {
            // Əgər loginRequest null-dursa, səhv mesajı qaytarılır
            if (loginRequest == null)
            {
                return BadRequest(new { message = "Etibarsız giriş sorğusu. Zəhmət olmasa düzgün formatda e-poçt və parol göndərin." });
            }

            // Veritabanında istifadəçi axtarılır
            var user = _context.Users.SingleOrDefault(u =>
                u.UserEmail == loginRequest.UserEmail &&
                u.UserPassword == loginRequest.UserPassword);

            // İstifadəçi tapılmadısa, qeyri-müəyyənlik (Unauthorized) statusu qaytarılır
            if (user == null)
            {
                return Unauthorized(new { message = "Yanlış e-posta vəya şifrə." });
            }

            // Giriş uğurlu olduqda istifadəçinin ID-sini və mesajını qaytarırıq
            return Ok(new { message = "Giriş Uğurlu", userId = user.UserId });
        }

        // İstifadəçinin profilini almaq üçün API
        [HttpGet("GetProfile/{userId}")]
        public IActionResult GetProfile(int userId)
        {
            // Veritabanında istifadəçi axtarılır
            var user = _context.Users.SingleOrDefault(u => u.UserId == userId);

            // Əgər istifadəçi tapılmadısa, tapılmadı mesajı qaytarılır
            if (user == null)
            {
                return NotFound(new { message = "İstifadəçi tapılmadı." });
            }

            // Profil məlumatlarını qaytarırıq
            return Ok(new
            {
                userName = user.UserName,
                userSurname = user.UserSurname,
                userEmail = user.UserEmail,
                userPhone = user.UserPhone
            });
        }

        // İstifadəçi profilini yeniləmək üçün API
        [HttpPut("UpdateProfile/{userId}")]
        public IActionResult UpdateProfile(int userId, [FromBody] User updateRequest)
        {
            // Veritabanında istifadəçi axtarılır
            var user = _context.Users.SingleOrDefault(u => u.UserId == userId);

            // Əgər istifadəçi tapılmadısa, tapılmadı mesajı qaytarılır
            if (user == null)
            {
                return NotFound(new { message = "İstifadəçi tapılmadı." });
            }

            // İstifadəçi məlumatları yenilənir
            user.UserName = updateRequest.UserName;
            user.UserSurname = updateRequest.UserSurname;
            user.UserEmail = updateRequest.UserEmail;
            user.UserPhone = updateRequest.UserPhone;

            // Yenilənmiş məlumatlar verilənlər bazasında saxlanılır
            _context.SaveChanges();

            // Yenilənmiş məlumatların təsdiqi
            return Ok(new
            {
                message = "Profil uğurla yeniləndi.",
            });
        }

        // Yeni istifadəçi qeydiyyatı API
        [HttpPost("Register")]
        public IActionResult Register([FromBody] User registerRequest)
        {
            // Qeydiyyat üçün lazımlı məlumatlar yoxlanılır
            if (registerRequest == null || string.IsNullOrEmpty(registerRequest.UserEmail) || string.IsNullOrEmpty(registerRequest.UserPassword))
            {
                return BadRequest(new { message = " E-poçt və parol məlumatları yoxdur." });
            }

            // Yeni istifadəçi obyekti yaradılır
            var user = new User
            {
                UserName = registerRequest.UserName,
                UserSurname = registerRequest.UserSurname,
                UserEmail = registerRequest.UserEmail,
                UserPhone = registerRequest.UserPhone,
                UserPassword = registerRequest.UserPassword
            };

            // Yeni istifadəçi verilənlər bazasına əlavə olunur
            _context.Users.Add(user);
            _context.SaveChanges();

            // Qeydiyyatın uğurlu olduğunu bildirən mesaj qaytarılır
            return Ok(new { message = "Qediyyat Uğurlu" });
        }

        // Logout (Çıxış) API
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Çıxış mesajı
            return Ok(new { message = "Çıxış Uğurlu." });
        }
    }
}
