using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Usersdata.Models;

public partial class UsersContext : DbContext
{
    public UsersContext()
    {
    }

    public UsersContext(DbContextOptions<UsersContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Basket> Baskets { get; set; }

    public virtual DbSet<BestSelling> BestSellings { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Color> Colors { get; set; }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Memory> Memories { get; set; }

    public virtual DbSet<Model> Models { get; set; }

    public virtual DbSet<NewProduct> NewProducts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<Statuss> Statusses { get; set; }

    public virtual DbSet<User> Users { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=DESKTOP-3NMTO8M\\MSSQLSERVER01;Database=users;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Basket>(entity =>
        {
            entity.HasKey(e => e.BasketId).HasName("PK__Basket__8FDA77D5FBEAC151");

            entity.ToTable("Basket");

            entity.Property(e => e.BasketId).HasColumnName("BasketID");
            entity.Property(e => e.AddedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.BestSellingId).HasColumnName("BestSellingID");
            entity.Property(e => e.NewProductId).HasColumnName("NewProductID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.BestSelling).WithMany(p => p.Baskets)
                .HasForeignKey(d => d.BestSellingId)
                .HasConstraintName("FK_Basket_BestSelling");

            entity.HasOne(d => d.NewProduct).WithMany(p => p.Baskets)
                .HasForeignKey(d => d.NewProductId)
                .HasConstraintName("FK_Basket_NewProduct");

            entity.HasOne(d => d.Product).WithMany(p => p.Baskets)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Basket_Product");

            entity.HasOne(d => d.User).WithMany(p => p.Baskets)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Basket__UserID__37703C52");
        });

        modelBuilder.Entity<BestSelling>(entity =>
        {
            entity.HasKey(e => e.BestSellingId).HasName("PK__BestSell__BA6B82CDCAF1BDB0");

            entity.ToTable("BestSelling");

            entity.Property(e => e.BestSellingId).HasColumnName("BestSellingID");
            entity.Property(e => e.BrandId).HasColumnName("BrandID");
            entity.Property(e => e.ModelColorId).HasColumnName("ModelColorID");
            entity.Property(e => e.ModelImgId).HasColumnName("ModelImgID");
            entity.Property(e => e.ModelMemoryId).HasColumnName("ModelMemoryID");
            entity.Property(e => e.ModelName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Rating).HasColumnType("decimal(3, 2)");

            entity.HasOne(d => d.Brand).WithMany(p => p.BestSellings)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BestSelli__Brand__02FC7413");

            entity.HasOne(d => d.ModelColor).WithMany(p => p.BestSellings)
                .HasForeignKey(d => d.ModelColorId)
                .HasConstraintName("FK__BestSelli__Model__04E4BC85");

            entity.HasOne(d => d.ModelImg).WithMany(p => p.BestSellings)
                .HasForeignKey(d => d.ModelImgId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BestSelli__Model__03F0984C");

            entity.HasOne(d => d.ModelMemory).WithMany(p => p.BestSellings)
                .HasForeignKey(d => d.ModelMemoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BestSelli__Model__05D8E0BE");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("PK__Brands__DAD4F3BE2BC37DED");

            entity.Property(e => e.BrandId).HasColumnName("BrandID");
            entity.Property(e => e.BrandName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Color>(entity =>
        {
            entity.HasKey(e => e.ColorId).HasName("PK__Colors__8DA7676DB0A0FD41");

            entity.Property(e => e.ColorId).HasColumnName("ColorID");
            entity.Property(e => e.ColorName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasKey(e => e.FavoriteId).HasName("PK__Favorite__CE74FAF5DDFF2EC7");

            entity.Property(e => e.FavoriteId).HasColumnName("FavoriteID");
            entity.Property(e => e.AddedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.BestSellingId).HasColumnName("BestSellingID");
            entity.Property(e => e.NewProductId).HasColumnName("NewProductID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.BestSelling).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.BestSellingId)
                .HasConstraintName("FK_Favorites_BestSelling");

            entity.HasOne(d => d.NewProduct).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.NewProductId)
                .HasConstraintName("FK_Favorites_NewProduct");

            entity.HasOne(d => d.Product).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Favorites__Produ__1CBC4616");

            entity.HasOne(d => d.User).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Favorites__UserI__1BC821DD");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__Images__7516F4ECA91A8CF5");

            entity.Property(e => e.ImageId).HasColumnName("ImageID");
            entity.Property(e => e.ImageColor).HasMaxLength(50);
        });

        modelBuilder.Entity<Memory>(entity =>
        {
            entity.HasKey(e => e.MemoryId).HasName("PK__Memories__9A4986B4E25B2928");

            entity.Property(e => e.MemoryId).HasColumnName("MemoryID");
            entity.Property(e => e.MemorySize)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Model>(entity =>
        {
            entity.HasKey(e => e.ModelId).HasName("PK__Models__E8D7A1CC4B98033F");

            entity.Property(e => e.ModelId).HasColumnName("ModelID");
            entity.Property(e => e.BrandId).HasColumnName("BrandID");
            entity.Property(e => e.ModelColorId).HasColumnName("ModelColorID");
            entity.Property(e => e.ModelImgId).HasColumnName("ModelImgID");
            entity.Property(e => e.ModelMemoryId).HasColumnName("ModelMemoryID");
            entity.Property(e => e.ModelName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Rating).HasColumnType("decimal(3, 2)");

            entity.HasOne(d => d.Brand).WithMany(p => p.Models)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Models__BrandID__6D0D32F4");

            entity.HasOne(d => d.ModelColor).WithMany(p => p.Models)
                .HasForeignKey(d => d.ModelColorId)
                .HasConstraintName("FK__Models__ModelCol__6EF57B66");

            entity.HasOne(d => d.ModelImg).WithMany(p => p.Models)
                .HasForeignKey(d => d.ModelImgId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Models__ModelImg__6E01572D");

            entity.HasOne(d => d.ModelMemory).WithMany(p => p.Models)
                .HasForeignKey(d => d.ModelMemoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Models__ModelMem__6FE99F9F");
        });

        modelBuilder.Entity<NewProduct>(entity =>
        {
            entity.HasKey(e => e.NewProductId).HasName("PK__NewProdu__334960D65657CA10");

            entity.Property(e => e.NewProductId).HasColumnName("NewProductID");
            entity.Property(e => e.BrandId).HasColumnName("BrandID");
            entity.Property(e => e.ModelColorId).HasColumnName("ModelColorID");
            entity.Property(e => e.ModelImgId).HasColumnName("ModelImgID");
            entity.Property(e => e.ModelMemoryId).HasColumnName("ModelMemoryID");
            entity.Property(e => e.ModelName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Rating).HasColumnType("decimal(3, 2)");

            entity.HasOne(d => d.Brand).WithMany(p => p.NewProducts)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NewProduc__Brand__0A9D95DB");

            entity.HasOne(d => d.ModelColor).WithMany(p => p.NewProducts)
                .HasForeignKey(d => d.ModelColorId)
                .HasConstraintName("FK__NewProduc__Model__0C85DE4D");

            entity.HasOne(d => d.ModelImg).WithMany(p => p.NewProducts)
                .HasForeignKey(d => d.ModelImgId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NewProduc__Model__0B91BA14");

            entity.HasOne(d => d.ModelMemory).WithMany(p => p.NewProducts)
                .HasForeignKey(d => d.ModelMemoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NewProduc__Model__0D7A0286");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6EDCD2D4285");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ModelId).HasColumnName("ModelID");

            entity.HasOne(d => d.Model).WithMany(p => p.Products)
                .HasForeignKey(d => d.ModelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__ModelI__1332DBDC");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.SaleId).HasName("PK__Sales__1EE3C41FC74B0904");

            entity.Property(e => e.SaleId).HasColumnName("SaleID");
            entity.Property(e => e.BestSellingId).HasColumnName("BestSellingID");
            entity.Property(e => e.NewProductId).HasColumnName("NewProductID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.SalesDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.BestSelling).WithMany(p => p.Sales)
                .HasForeignKey(d => d.BestSellingId)
                .HasConstraintName("FK_BestSelling");

            entity.HasOne(d => d.NewProduct).WithMany(p => p.Sales)
                .HasForeignKey(d => d.NewProductId)
                .HasConstraintName("FK_NewProduct");

            entity.HasOne(d => d.Product).WithMany(p => p.Sales)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Product");

            entity.HasOne(d => d.User).WithMany(p => p.Sales)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Sales__UserID__2645B050");
        });

        modelBuilder.Entity<Statuss>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__Statuss__C8EE2063FABF15CE");

            entity.ToTable("Statuss");

            entity.Property(e => e.StatusName).HasMaxLength(15);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CC1572D38");

            entity.HasIndex(e => e.UserEmail, "UQ__Users__08638DF8F9ADE147").IsUnique();

            entity.HasIndex(e => e.UserPhone, "UQ__Users__F2577C4705789284").IsUnique();

            entity.Property(e => e.UserEmail)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserName).HasMaxLength(15);
            entity.Property(e => e.UserPassword).HasMaxLength(200);
            entity.Property(e => e.UserPhone)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.UserSurname).HasMaxLength(20);

            entity.HasOne(d => d.UserStatus).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserStatusId)
                .HasConstraintName("FK__Users__UserStatu__5165187F");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
