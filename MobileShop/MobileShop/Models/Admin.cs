﻿using System;
using System.Collections.Generic;

namespace MobileShop.Models;

public partial class Admin
{
    public int AdminId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public DateTime? CreatedAt { get; set; }
}
