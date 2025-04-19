using System;
using System.Collections.Generic;

namespace Usersdata.Models;

public partial class Model
{
    public int ModelId { get; set; }

    public int BrandId { get; set; }

    public string ModelName { get; set; } = null!;

    public int ModelImgId { get; set; }

    public int? ModelColorId { get; set; }

    public int ModelMemoryId { get; set; }

    public int Price { get; set; }

    public int CreditPrice { get; set; }

    public decimal? Rating { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual Color? ModelColor { get; set; }

    public virtual Image ModelImg { get; set; } = null!;

    public virtual Memory ModelMemory { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
