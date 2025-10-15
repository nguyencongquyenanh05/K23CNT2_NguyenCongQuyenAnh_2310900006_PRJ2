using Microsoft.AspNetCore.Mvc;
using MobileShop.Models;
using Newtonsoft.Json;

namespace MobileShop.Controllers
{
    public class GioHangController : Controller
    {
        private readonly MobileShopDbContext _context;

        public GioHangController(MobileShopDbContext context)
        {
            _context = context;
        }

        // 🧺 Trang giỏ hàng
        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        // ➕ Thêm sản phẩm vào giỏ
        public IActionResult ThemVaoGio(int id)
        {
            var sanPham = _context.SanPhams.FirstOrDefault(sp => sp.MaSp == id);
            if (sanPham == null)
            {
                return NotFound("Không tìm thấy sản phẩm.");
            }

            var cart = GetCart();
            var existingItem = cart.FirstOrDefault(x => x.MaSp == id);

            if (existingItem != null)
            {
                existingItem.SoLuong++;
            }
            else
            {
                cart.Add(new GioHangItem
                {
                    MaSp = sanPham.MaSp,
                    TenSp = sanPham.TenSp ?? "Sản phẩm không tên",
                    Gia = sanPham.Gia ?? 0, // ✅ Nếu nullable
                    SoLuong = 1,
                    HinhAnh = sanPham.HinhAnh ?? "no-image.png"
                });
            }

            SaveCart(cart);
            return RedirectToAction("Index");
        }

        // ➖ Giảm số lượng hoặc xóa sản phẩm
        public IActionResult GiamSoLuong(int id)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(x => x.MaSp == id);
            if (item != null)
            {
                item.SoLuong--;
                if (item.SoLuong <= 0)
                    cart.Remove(item);
            }

            SaveCart(cart);
            return RedirectToAction("Index");
        }

        // ❌ Xóa 1 sản phẩm khỏi giỏ
        public IActionResult XoaKhoiGio(int id)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(x => x.MaSp == id);
            if (item != null)
            {
                cart.Remove(item);
            }

            SaveCart(cart);
            return RedirectToAction("Index");
        }

        // 🔄 Xóa toàn bộ giỏ hàng
        public IActionResult XoaTatCa()
        {
            HttpContext.Session.Remove("Cart");
            return RedirectToAction("Index");
        }

        // ======================================================
        // 🧩 HÀM HỖ TRỢ (Session)
        private List<GioHangItem> GetCart()
        {
            var sessionData = HttpContext.Session.GetString("Cart");
            if (string.IsNullOrEmpty(sessionData))
                return new List<GioHangItem>();

            return JsonConvert.DeserializeObject<List<GioHangItem>>(sessionData)
                   ?? new List<GioHangItem>();
        }

        private void SaveCart(List<GioHangItem> cart)
        {
            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
        }
    }

    // ======================================================
    // 🔹 Model giỏ hàng (đặt dưới controller hoặc trong Models/)
    public class GioHangItem
    {
        public int MaSp { get; set; }
        public string TenSp { get; set; } = string.Empty;
        public decimal Gia { get; set; }
        public int SoLuong { get; set; }
        public string? HinhAnh { get; set; }
    }
}
