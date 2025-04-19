using System;
using System.Collections.Generic;

namespace Usersdata.Models;

public partial class BestSelling
{
    public int BestSellingId { get; set; }

    public int BrandId { get; set; }

    public string ModelName { get; set; } = null!;

    public int ModelImgId { get; set; }

    public int? ModelColorId { get; set; }

    public int ModelMemoryId { get; set; }

    public int Price { get; set; }

    public int CreditPrice { get; set; }

    public decimal? Rating { get; set; }

    public int Stock { get; set; }

    public virtual ICollection<Basket> Baskets { get; set; } = new List<Basket>();

    public virtual Brand Brand { get; set; } = null!;

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual Color? ModelColor { get; set; }

    public virtual Image ModelImg { get; set; } = null!;

    public virtual Memory ModelMemory { get; set; } = null!;

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
