using System;
using System.Collections.Generic;

namespace Usersdata.Models;

public partial class Image
{
    public int ImageId { get; set; }

    public byte[] ImageData { get; set; } = null!;

    public string? ImageColor { get; set; }

    public virtual ICollection<BestSelling> BestSellings { get; set; } = new List<BestSelling>();

    public virtual ICollection<Model> Models { get; set; } = new List<Model>();

    public virtual ICollection<NewProduct> NewProducts { get; set; } = new List<NewProduct>();
}
