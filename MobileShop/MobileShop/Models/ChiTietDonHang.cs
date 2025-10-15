using System;
using System.Collections.Generic;

namespace MobileShop.Models;

public partial class ChiTietDonHang
{
    public int MaCt { get; set; }

    public int? MaDh { get; set; }

    public int? MaSp { get; set; }

    public int? SoLuong { get; set; }

    public decimal? DonGia { get; set; }

    public virtual DonHang? MaDhNavigation { get; set; }

    public virtual SanPham? MaSpNavigation { get; set; }
}
