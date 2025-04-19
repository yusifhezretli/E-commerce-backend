using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Usersdata.Models;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace Usersdata.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly UsersContext _context;

        public ProductController(UsersContext context)
        {
            _context = context;
        }

        // GET: api/BestSelling
        [HttpGet("BestSell")]
        public async Task<IActionResult> BestSell(int? id = null)
        {
            if (id.HasValue)
            {
                // Əgər ID mövcuddursa, biz yalnız həmin ID ilə məhsulu əldə edirik
                var product = await _context.BestSellings
                    .Include(p => p.ModelImg) // Şəkil məlumatı
                    .Include(p => p.ModelColor) // Rəng məlumatı
                    .Include(p => p.ModelMemory) // Yaddaş məlumatı
                    .Where(p => p.BestSellingId == id.Value) // ID'ye görə filtreleme
                    .Select(p => new
                    {
                        p.BestSellingId,
                        p.ModelName,
                        ColorName = p.ModelColor != null ? p.ModelColor.ColorName : "rəng yoxdur",
                        MemorySize = p.ModelMemory != null ? p.ModelMemory.MemorySize : "Yaddaş yoxdur",
                        p.Price,
                        p.CreditPrice,
                        p.Rating,
                        ImageUrl = SaveImage(p.ModelImg.ImageData) // Şəkli URL olaraq qaytarın
                    })
                    .FirstOrDefaultAsync();

                if (product == null)
                {
                    return NotFound(new { Message = "Məhsul tapılmadı." });
                }

                return Ok(product); // Tək məhsulu qaytarırıq
            }
            else
            {
                // ID yoxdursa, bütün məhsulları alırıq
                var allProducts = await _context.BestSellings
                    .Include(p => p.ModelImg) 
                    .Include(p => p.ModelColor) 
                    .Include(p => p.ModelMemory) 
                    .Where(p => p.ModelImg != null) // Şəkilsiz məhsulları istisna edirik
                    .Select(p => new
                    {
                        p.BestSellingId,
                        p.ModelName,
                        ColorName = p.ModelColor != null ? p.ModelColor.ColorName : "rəng yoxdur",
                        MemorySize = p.ModelMemory != null ? p.ModelMemory.MemorySize : "Yaddaş yoxdur",
                        p.Price,
                        p.CreditPrice,
                        p.Rating,
                        ImageUrl = SaveImage(p.ModelImg.ImageData) 
                    })
                    .ToListAsync();

                if (allProducts == null || allProducts.Count == 0)
                {
                    return NotFound(new { Message = "Məhsul tapılmadı." });
                }

                return Ok(allProducts); // Bütün məhsulları geri qaytarırıq
            }
        }



        [HttpGet("NewProduct")]
        public async Task<IActionResult> NewProduct(int? id = null)
        {
            if (id.HasValue)
            {
                var product = await _context.NewProducts
                    .Include(p => p.ModelImg)  
                    .Include(p => p.ModelColor) 
                    .Include(p => p.ModelMemory) 
                    .Where(p => p.NewProductId == id.Value) 
                    .Select(p => new
                    {
                        p.NewProductId,
                        p.ModelName,
                        ColorName = p.ModelColor != null ? p.ModelColor.ColorName : "rəng yoxdur",
                        MemorySize = p.ModelMemory != null ? p.ModelMemory.MemorySize : "Yaddaş yoxdur",
                        p.Price,
                        p.CreditPrice,
                        p.Rating,
                        ImageUrl = SaveImage(p.ModelImg.ImageData) 
                    })
                    .FirstOrDefaultAsync();

                if (product == null)
                {
                    return NotFound(new { Message = "Məhsul tapılmadı." });
                }

                return Ok(product); 
            }
            else
            {
           
                var allProducts = await _context.NewProducts
                    .Include(p => p.ModelImg) 
                    .Include(p => p.ModelColor) 
                    .Include(p => p.ModelMemory) 
                    .Where(p => p.ModelImg != null) 
                    .Select(p => new
                    {
                        p.NewProductId,
                        p.ModelName,
                        ColorName = p.ModelColor != null ? p.ModelColor.ColorName : "rəng yoxdur",
                        MemorySize = p.ModelMemory != null ? p.ModelMemory.MemorySize : "Yaddaş yoxdur",
                        p.Price,
                        p.CreditPrice,
                        p.Rating,
                        ImageUrl = SaveImage(p.ModelImg.ImageData) 
                    })
                    .ToListAsync();

                if (allProducts == null || allProducts.Count == 0)
                {
                    return NotFound(new { Message = "Məhsul tapılmadı." });
                }

                return Ok(allProducts);
            }
        }



        [HttpGet("AllProduct")]
        public async Task<IActionResult> AllProduct(int? id = null)
        {
            if (id.HasValue)
            {
              
                var product = await _context.Products
                    .Include(p => p.Model) 
                    .ThenInclude(m => m.ModelImg)
                    .Include(p => p.Model.ModelColor) 
                    .Include(p => p.Model.ModelMemory) 
                    .Where(p => p.ProductId == id.Value) 
                    .Select(p => new
                    {
                        p.ProductId,
                        p.Model.ModelName,
                        ColorName = p.Model.ModelColor != null ? p.Model.ModelColor.ColorName : "rəng yoxdur",
                        MemorySize = p.Model.ModelMemory != null ? p.Model.ModelMemory.MemorySize : "Yaddaş yoxdur",
                        p.Model.Price,
                        p.Model.CreditPrice,
                        p.Model.Rating,
                        ImageUrl = p.Model.ModelImg != null ? SaveImage(p.Model.ModelImg.ImageData) : null 
                    })
                    .FirstOrDefaultAsync();

                if (product == null)
                {
                    return NotFound(new { Message = "Məhsul tapılmadı." });
                }

                return Ok(product); 
            }
            else
            {
                
                var allProducts = await _context.Products
                    .Include(p => p.Model) 
                    .ThenInclude(m => m.ModelImg) 
                    .Include(p => p.Model.ModelColor) 
                    .Include(p => p.Model.ModelMemory) 
                    .Select(p => new
                    {
                        p.ProductId,
                        p.Model.ModelName,
                        ColorName = p.Model.ModelColor != null ? p.Model.ModelColor.ColorName : "Renk yok",
                        MemorySize = p.Model.ModelMemory != null ? p.Model.ModelMemory.MemorySize : "Yaddaş yoxdur",
                        p.Model.Price,
                        p.Model.CreditPrice,
                        p.Model.Rating,
                        ImageUrl = p.Model.ModelImg != null ? SaveImage(p.Model.ModelImg.ImageData) : null 
                    })
                    .ToListAsync();

                if (allProducts == null || allProducts.Count == 0)
                {
                    return NotFound(new { Message = "Məhsul tapılmadı." });
                }

                return Ok(allProducts); 
            }
        }



        // Şəkli SHA-256 ilə saxlayan və URL-ni qaytaran funksiya
        public static string SaveImage(byte[] imageData)
        {
            var fileName = GetFileName(imageData); // SHA-256 ilə fayl adını yaradırıq
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName); //  fayl yolunu yaradırıq



            // Əgər fayl  mövcuddursa yenidən yazmırıq
            if (!System.IO.File.Exists(filePath))
            {
                // faylı yaddasda saxlayırıq
                System.IO.File.WriteAllBytes(filePath, imageData); // sekilleri qovluqa atırıq
            }

            //  faylın URL-si 
            return "/images/" + fileName;
        }

        // SHA-256 alqoritmindən istifadə edərək şəkil faylı üçün unikal ad yaradılır
        public static string GetFileName(byte[] imageData)
        {
            using var sha256 = SHA256.Create();
            // Verilmiş imageData-dan hash dəyərinin hesablanması
            var hash = sha256.ComputeHash(imageData);

            // fayl adı kimi hash dəyərindən istifadə edirlir
            return BitConverter.ToString(hash).Replace("-", "").ToLower() + ".png";
        }




        [HttpPost("AddToBasket")]
        public async Task<IActionResult> AddToBasket([FromBody] Basket basket)
        {
            // İstifadəçinin user mellumatlarını yoxlayram
            var userId = basket.UserId;
            if (basket.UserId == null)
            {
                return Unauthorized("daxil olmadan məhsulu səbətinizə əlavə edə bilməzsiniz.");
            }

            // Verilənlər bazasına saxlamazdan əvvəl etibarlı UserId-i yoxlayır
            var user = await _context.Users.FindAsync(userId!.Value); // Artıq null olmadığı üçün userId.Value kimi istifadə olunur
            if (user == null)
            {
                return Unauthorized("Yanlış istifadəçi. Zəhmət olmasa yenidən daxil olun.");
            }
            //userin idsini sebete elave edirem
            basket.UserId = userId.Value;

            // mehsulları secirem en az 1 denesini
            if (basket.ProductId == null && basket.NewProductId == null && basket.BestSellingId == null)
            {
                return BadRequest("Ən azı bir məhsul ID-si təqdim edilməlidir.");
            }

            // tarix
            basket.AddedDate = DateTime.Now;


            // Məhsulun artıq səbətdə olub olmadığını yoxla
            var existingBasketItem = await _context.Baskets
                .Where(b => b.UserId == userId)
                .Where(b =>
                    (b.ProductId != null && b.ProductId == basket.ProductId) ||
                    (b.NewProductId != null && b.NewProductId == basket.NewProductId) ||
                    (b.BestSellingId != null && b.BestSellingId == basket.BestSellingId)
                )
                .FirstOrDefaultAsync();

            if (existingBasketItem != null)
            {
                // Mövcud məhsulun quantity-ni artır
                existingBasketItem.Quantity += basket.Quantity;
                await _context.SaveChangesAsync();
                return Ok(new { message = "Məhsul artıq səbətdədir. Miqdar yeniləndi!" });
            }

            // mehsulların stok deyerni idare edir  quantitye gore ona gore azaldıram
            if (basket.ProductId != null)
            {
                var product = await _context.Products.FindAsync(basket.ProductId);
                if (product == null)
                {
                    return NotFound("Məhsul tapılmadı.");
                }
                if (product.Stock < basket.Quantity)
                {
                    return BadRequest("Stokda kifayət qədər məhsul yoxdur.");
                }
                product.Stock -= basket.Quantity;
            }
            else if (basket.NewProductId != null)
            {
                var newProduct = await _context.NewProducts.FindAsync(basket.NewProductId);
                if (newProduct == null)
                {
                    return NotFound("Məhsul tapılmadı.");
                }
                if (newProduct.Stock < basket.Quantity)
                {
                    return BadRequest("Stokda kifayət qədər məhsul yoxdur.");
                }
                newProduct.Stock -= basket.Quantity; // Stoktan miqtarı azalt
            }
            else if (basket.BestSellingId != null)
            {
                var bestSellingProduct = await _context.BestSellings.FindAsync(basket.BestSellingId);
                if (bestSellingProduct == null)
                {
                    return NotFound("Məhsul tapılmadı.");
                }
                if (bestSellingProduct.Stock < basket.Quantity)
                {
                    return BadRequest("Stokda kifayət qədər məhsul yoxdur.");
                }
                bestSellingProduct.Stock -= basket.Quantity;
            }

            // Səbəti sqla at
            _context.Baskets.Add(basket);

            await _context.SaveChangesAsync();

            return Ok(new { message = "Məhsul uğurla səbətə əlavə edildi və ehtiyat yeniləndi!" });
        }



        [HttpGet("user-basket/{userId}")]
        public async Task<IActionResult> GetUserBasket(int userId)
        {
            // Verilən `UserId`-ni yoxlayırıq
            var userExists = await _context.Users.AnyAsync(u => u.UserId == userId);
            if (!userExists)
            {
                return Unauthorized("Yanlış istifadəçi. Zəhmət olmasa yenidən daxil olun.");
            }

            // İstifadəçinin səbət məlumatlarını əldə edirik
            var basketItems = await _context.Baskets
                .Where(b => b.UserId == userId) // İstifadəçinin məhsullarını süz
                .Include(b => b.Product) // Əsas məhsul məlumatları
                .Include(b => b.NewProduct) // Yeni məhsul məlumatları
                .Include(b => b.BestSelling) // Bestseller məhsul məlumatları
                .Select(b => new
                {
                    BasketId = b.BasketId,
                    Quantity = b.Quantity,
                    AddedDate = b.AddedDate,

                    // Məhsul Detalları (Product)
                    ProductDetails = b.Product != null
                        ? new
                        {
                            b.Product.ProductId,
                            ModelName = b.Product.Model != null ? b.Product.Model.ModelName : "Model məlumatı yoxdur",
                            Price = b.Product.Model != null ? b.Product.Model.Price : 0,
                            CreditPrice = b.Product.Model != null ? b.Product.Model.CreditPrice : 0,
                            ImageUrl = b.Product.Model != null && b.Product.Model.ModelImg != null
                                       ? SaveImage(b.Product.Model.ModelImg.ImageData)
                                       : null
                        }
                        : null,

                    // Yeni Məhsul Detalları (NewProduct)
                    NewProductDetails = b.NewProduct != null
                        ? new
                        {
                            b.NewProduct.NewProductId,
                            ModelName = b.NewProduct.ModelName ?? "Yeni Məhsul modeli yoxdur",
                            Price = b.NewProduct.Price,
                            CreditPrice = b.NewProduct.CreditPrice,
                            ImageUrl = b.NewProduct.ModelImg != null
                                       ? SaveImage(b.NewProduct.ModelImg.ImageData)
                                       : null
                        }
                        : null,

                    // Bestseller Məhsul Detalları (BestSelling) - Model məlumatlarını daxil etmədən
                    BestSellingDetails = b.BestSelling != null
                        ? new
                        {
                            b.BestSelling.BestSellingId,
                            ModelName = b.BestSelling.ModelName ?? "Bestseller modeli yoxdur",
                            Price = b.BestSelling.Price,
                            CreditPrice = b.BestSelling.CreditPrice,
                            ImageUrl = b.BestSelling.ModelImg != null
                                       ? SaveImage(b.BestSelling.ModelImg.ImageData)
                                       : null
                        }
                        : null
                })
                .ToListAsync();

            if (!basketItems.Any())
            {
                return Ok("Səbətinizdə məhsul yoxdur.");
            }

            // Məhsulları JSON olaraq qaytarırıq
            return Ok(basketItems);
        }



        [HttpDelete("delete-basket/{basketId}")]
        public async Task<IActionResult> DeleteBasketItem(int basketId)
        {
            
            var basket = await _context.Baskets.FindAsync(basketId);
            if (basket == null)
            {
                return NotFound("Sebet elementi tapılmadı.");
            }

            // Stokları yenilə
            if (basket.ProductId != null)
            {
                var product = await _context.Products.FindAsync(basket.ProductId);
                if (product == null)
                {
                    return NotFound("Məhsul tapılmadı.");
                }

                // Silinmiş səbətin miqdarını anbara əlavə edin
                product.Stock += basket.Quantity;
            }

            if (basket.NewProductId != null)
            {
                var newProduct = await _context.NewProducts.FindAsync(basket.NewProductId);
                if (newProduct == null)
                {
                    return NotFound("Yeni məhsul tapılmadı.");
                }

              
                newProduct.Stock += basket.Quantity;
            }

            if (basket.BestSellingId != null)
            {
                var bestSelling = await _context.BestSellings.FindAsync(basket.BestSellingId);
                if (bestSelling == null)
                {
                    return NotFound("Ən çox satılan məhsul tapılmadı.");
                }

             
                bestSelling.Stock += basket.Quantity;
            }

            // Səbət elementini silin
            _context.Baskets.Remove(basket);
            await _context.SaveChangesAsync();

            return Ok("Sebet elementi silindi və stok yeniləndi.");
        }


        [HttpPost("AddToFavorites")]
        public async Task<IActionResult> AddToFavorites([FromBody] Favorite favorite)
        {
            // İstifadəçinin user məlumatlarını yoxlayırıq
            var userId = favorite.UserId;
            if (userId == null)
            {
                return Unauthorized("Əvvəlcə daxil olun.");
            }

            // Verilənlər bazasında etibarlı UserId-i yoxlayır
            var user = await _context.Users.FindAsync(userId.Value);
            if (user == null)
            {
                return Unauthorized("Keçərsiz istifadəçi.");
            }

            // UserId əlavə edilir
            favorite.UserId = userId.Value;

            // Məhsul ID yoxlanılır
            if (favorite.ProductId == null && favorite.NewProductId == null && favorite.BestSellingId == null)
            {
                return BadRequest("Məhsul ID tələb olunur.");
            }


            // Mövcud favoritlər arasında yoxlanış
            var existingFavorite = await _context.Favorites
                .FirstOrDefaultAsync(f =>
                    f.UserId == userId.Value &&
                    ((favorite.ProductId != null && f.ProductId == favorite.ProductId) ||
                    (favorite.NewProductId != null && f.NewProductId == favorite.NewProductId) ||
                    (favorite.BestSellingId != null && f.BestSellingId == favorite.BestSellingId)));

            if (existingFavorite != null)
            {
                return Conflict("Bu məhsul artıq favoritlərinizdədir.");
            }



            // Məhsulun mövcudluğunu yoxla
            if (favorite.ProductId != null)
            {
                var product = await _context.Products.FindAsync(favorite.ProductId);
                if (product == null)
                {
                    return NotFound("Məhsul tapılmadı.");
                }
            }
            else if (favorite.NewProductId != null)
            {
                var newProduct = await _context.NewProducts.FindAsync(favorite.NewProductId);
                if (newProduct == null)
                {
                    return NotFound("Məhsul tapılmadı.");
                }
            }
            else if (favorite.BestSellingId != null)
            {
                var bestSellingProduct = await _context.BestSellings.FindAsync(favorite.BestSellingId);
                if (bestSellingProduct == null)
                {
                    return NotFound("Məhsul tapılmadı.");
                }
            }

            // Favoritləri veritabanına əlavə edirik
            _context.Favorites.Add(favorite);

            await _context.SaveChangesAsync();

            return Ok(new { message = "Məhsul favoritlərə əlavə edildi!" });
        }


        [HttpGet("user-favorites/{userId}")]
        public async Task<IActionResult> GetUserFavorites(int userId)
        {
            // Verilmiş `UserId`-i yoxlayırıq
            var userExists = await _context.Users.AnyAsync(u => u.UserId == userId);
            if (!userExists)
            {
                return Unauthorized("etibarsız istifadəçi.");
            }

            // İstifadəçinin favorilərini əldə edirik
            var favoriteItems = await _context.Favorites
                .Where(f => f.UserId == userId) // İstifadəçinin favorilərini süz
                .Include(f => f.Product) // Əsas məhsul məlumatları
                .Include(f => f.NewProduct) // Yeni məhsul məlumatları
                .Include(f => f.BestSelling) // Bestseller məhsul məlumatları
                .Select(f => new
                {
                    FavoriteId = f.FavoriteId,
                    AddedDate = f.AddedDate,

                    // Məhsul Detalları (Product)
                    ProductDetails = f.Product != null
                        ? new
                        {
                            f.Product.ProductId,
                            ModelName = f.Product.Model != null ? f.Product.Model.ModelName : "Model məlumatı yoxdur",
                            Price = f.Product.Model != null ? f.Product.Model.Price : 0,
                            CreditPrice = f.Product.Model != null ? f.Product.Model.CreditPrice : 0,
                            ImageUrl = f.Product.Model != null && f.Product.Model.ModelImg != null
                                       ? SaveImage(f.Product.Model.ModelImg.ImageData)
                                       : null
                        }
                        : null,

                    // Yeni Məhsul Detalları (NewProduct)
                    NewProductDetails = f.NewProduct != null
                        ? new
                        {
                            f.NewProduct.NewProductId,
                            ModelName = f.NewProduct.ModelName ?? "Yeni Məhsul modeli yoxdur",
                            Price = f.NewProduct.Price,
                            CreditPrice = f.NewProduct.CreditPrice,
                            ImageUrl = f.NewProduct.ModelImg != null
                                       ? SaveImage(f.NewProduct.ModelImg.ImageData)
                                       : null
                        }
                        : null,

                    // Bestseller Məhsul Detalları (BestSelling) - Model məlumatlarını daxil etmədən
                    BestSellingDetails = f.BestSelling != null
                        ? new
                        {
                            f.BestSelling.BestSellingId,
                            ModelName = f.BestSelling.ModelName ?? "Bestseller modeli yoxdur",
                            Price = f.BestSelling.Price,
                            CreditPrice = f.BestSelling.CreditPrice,
                            ImageUrl = f.BestSelling.ModelImg != null
                                       ? SaveImage(f.BestSelling.ModelImg.ImageData)
                                       : null
                        }
                        : null
                })
                .ToListAsync();

            if (!favoriteItems.Any())
            {
                return Ok("Favorileriniz boşdur.");
            }

            // Məhsulları JSON olaraq qaytarırıq
            return Ok(favoriteItems);
        }


        [HttpDelete("delete-favorite/{favoriteId}")]
        public async Task<IActionResult> DeleteFavoriteItem(int favoriteId)
        {
        
            var favorite = await _context.Favorites.FindAsync(favoriteId);
            if (favorite == null)
            {
                return NotFound("Favori elementi tapılmadı.");
            }

          
            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync();

            return Ok("Favori elementi silindi.");
        }





    }
}
