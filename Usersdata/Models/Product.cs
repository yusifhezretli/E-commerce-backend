using System;
using System.Collections.Generic;

namespace Usersdata.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public int ModelId { get; set; }

    public int Stock { get; set; }

    public virtual ICollection<Basket> Baskets { get; set; } = new List<Basket>();

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual Model Model { get; set; } = null!;

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
