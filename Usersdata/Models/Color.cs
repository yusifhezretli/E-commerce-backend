using System;
using System.Collections.Generic;

namespace Usersdata.Models;

public partial class Color
{
    public int ColorId { get; set; }

    public string? ColorName { get; set; }

    public virtual ICollection<BestSelling> BestSellings { get; set; } = new List<BestSelling>();

    public virtual ICollection<Model> Models { get; set; } = new List<Model>();

    public virtual ICollection<NewProduct> NewProducts { get; set; } = new List<NewProduct>();
}
