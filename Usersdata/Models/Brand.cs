using System;
using System.Collections.Generic;

namespace Usersdata.Models;

public partial class Brand
{
    public int BrandId { get; set; }

    public string BrandName { get; set; } = null!;

    public virtual ICollection<BestSelling> BestSellings { get; set; } = new List<BestSelling>();

    public virtual ICollection<Model> Models { get; set; } = new List<Model>();

    public virtual ICollection<NewProduct> NewProducts { get; set; } = new List<NewProduct>();
}
