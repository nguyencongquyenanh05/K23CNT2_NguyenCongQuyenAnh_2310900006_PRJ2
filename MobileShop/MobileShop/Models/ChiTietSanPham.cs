using System;
using System.Collections.Generic;

namespace MobileShop.Models;

public partial class ChiTietSanPham
{
    public int MaCtsp { get; set; }

    public int MaSp { get; set; }

    public string? CauHinh { get; set; }

    public string? MoTaChiTiet { get; set; }

    public int? SoLuongTon { get; set; }

    public string? MauSac { get; set; }

    public virtual SanPham MaSpNavigation { get; set; } = null!;
}
