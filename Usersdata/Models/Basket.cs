using System;
using System.Collections.Generic;

namespace Usersdata.Models;

public partial class Basket
{
    public int BasketId { get; set; }

    public int? UserId { get; set; }

    public int? ProductId { get; set; }

    public int? NewProductId { get; set; }

    public int? BestSellingId { get; set; }

    public int Quantity { get; set; }

    public DateTime? AddedDate { get; set; }

    public virtual BestSelling? BestSelling { get; set; }

    public virtual NewProduct? NewProduct { get; set; }

    public virtual Product? Product { get; set; }

    public virtual User? User { get; set; }
}
