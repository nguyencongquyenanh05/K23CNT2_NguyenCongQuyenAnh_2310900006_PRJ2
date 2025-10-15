using System;
using System.Collections.Generic;

namespace MobileShop.Models;

public partial class DanhMuc
{
    public int MaDanhMuc { get; set; }

    public string? TenDanhMuc { get; set; }

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
