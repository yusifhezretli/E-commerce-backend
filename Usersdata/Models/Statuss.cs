﻿using System;
using System.Collections.Generic;

namespace Usersdata.Models;

public partial class Statuss
{
    public int StatusId { get; set; }

    public string? StatusName { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
