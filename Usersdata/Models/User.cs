using System;
using System.Collections.Generic;

namespace Usersdata.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string? UserSurname { get; set; }

    public string? UserEmail { get; set; }

    public string? UserPhone { get; set; }

    public string? UserPassword { get; set; }

    public int? UserStatusId { get; set; }

    public virtual ICollection<Basket> Baskets { get; set; } = new List<Basket>();

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    public virtual Statuss? UserStatus { get; set; }
}
